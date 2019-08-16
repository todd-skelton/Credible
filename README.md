[![](https://img.shields.io/nuget/v/Credible.svg)](https://www.nuget.org/packages/Credible) [![](https://img.shields.io/nuget/vpre/Credible.svg)](https://www.nuget.org/packages/Credible)

# Credible
Simple Json Web Token library for .NET

## Installation
### Package Manager
`Install-Package Credible`

### .NET CLI
`dotnet add package Credible`

## Getting Started
1. Create an identity model to be used in your application.
```csharp
public class UserIdentity
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}
```
2. Create a payload factory to convert your identity model into a token.
```csharp
public class PayloadFactory : IPayloadFactory<UserIdentity>
{
    public IDictionary<string, object> Create(UserIdentity identity)
    {
        return new Dictionary<string, object>
        {
            { "userId", identity.UserId },
            { "username", identity.Username },
            { "permissions", identity.Permissions }
        };
    }
}
```

3. Create a user factory to convert your token back into an identity.
```csharp
public class UserIdentityFactory : IIdentityFactory<UserIdentity>
{
    public UserIdentity Create(ClaimsPrincipal principal)
    {
        return new UserIdentity
        {
            UserId = int.Parse(principal.FindFirst("userId")?.Value ?? "0"),
            Username = principal.FindFirst("username")?.Value ?? "",
            Permissions = principal.FindAll("permissions").Select(e => e.Value)
        };
    }
}
```

4. Configure authentication in `Startup.cs`. Make sure you add `app.UseAuthentication()` above `app.UseMvc()` in `Configure`.
```csharp
public class Startup
{
    //...
    public void ConfigureServices(IServiceCollection services)
    {
        //...
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("My super secret key."));

        services.AddAuthentication("Bearer")
            .AddCredible<UserIdentity, UserIdentityFactory, PayloadFactory>("Bearer",
                issuingOptions =>
                {
                    issuingOptions.Audience = "WebApiSample";
                    issuingOptions.Issuer = "WebApiSample";
                    issuingOptions.Expiration = TimeSpan.FromMinutes(30);
                    issuingOptions.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                },
                validationOptions =>
                {
                    validationOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,
                        ValidIssuer = "WebApiSample",
                        ValidAudience = "WebApiSample",
                        NameClaimType = "username"
                    };
                    validationOptions.Audience = "WebApiSample";
                    validationOptions.ClaimsIssuer = "WebApiSample";
                    validationOptions.Challenge = "Bearer";
                }
            );
        //...
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        //...
        app.UseAuthentication();
        app.UseMvc();
        //...
    }
}
```

5. Inject your `JsonWebTokenFactory<TIdentity>` into your controller and call the `create` method to create a new token based on the identity model.
```csharp
[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly JsonWebTokenFactory<UserIdentity> _tokenFactory;

    public TokenController(JsonWebTokenFactory<UserIdentity> tokenFactory)
    {
        _tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
    }

    // GET api/token
    [HttpPost("")]
    public ActionResult<JsonWebToken> Get(UserIdentity identity)
    {
        return _tokenFactory.Create(identity);
    }
}
```

6. Inject your identity class into a controller to use it to get the current user.
```csharp
[Route("api/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserIdentity _user;

    public UserController(UserIdentity user)
    {
        _user = user;
    }

    // GET api/user
    [HttpGet("")]
    public ActionResult<UserIdentity> Get()
    {
        return _user;
    }
}
```

## Separate Issuing and Validation
If you want to separate issue and validation, you can configure them separately.

### Validation Only
Validation requires the authentication middleware, so be sure to add `app.UseAuthentication();` like the example above.
```csharp
services.AddAuthentication("Bearer")
    .AddCredible<UserIdentity, UserIdentityFactory>("Bearer",
        validationOptions =>
        {
            validationOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidIssuer = "WebApiSample",
                ValidAudience = "WebApiSample",
                NameClaimType = "username"
            };
            validationOptions.Audience = "WebApiSample";
            validationOptions.ClaimsIssuer = "WebApiSample";
            validationOptions.Challenge = "Bearer";
        }
    );
```

## Issuing Only
Issuing doesn't require the authentication middleware or the `AuthenticationBuilder` to be used. Just call `AddCredible` on the `IServiceCollection` directly.
```csharp
services.AddCredible<UserIdentity, PayloadFactory>(
    issueOptions =>
    {
        issueOptions.Audience = "WebApiSample";
        issueOptions.Issuer = "WebApiSample";
        issueOptions.Expiration = TimeSpan.FromMinutes(30);
        issueOptions.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }
);
```