using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Credible
{
    /// <summary>
    /// Extensions to build authentication
    /// </summary>
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TClaimsFactory">The type of claims factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureIssueOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddJwtBearer<TIdentity, TIdentityFactory, TClaimsFactory>(this AuthenticationBuilder builder, Action<JsonWebTokenFactoryOptions> configureIssueOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TClaimsFactory : class, IClaimsFactory<TIdentity>
            => builder.AddJwtBearer<TIdentity, TIdentityFactory, TClaimsFactory>(JwtBearerDefaults.AuthenticationScheme, configureIssueOptions, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TClaimsFactory">The type of claims factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddJwtBearer<TIdentity, TIdentityFactory, TClaimsFactory>(this AuthenticationBuilder builder, string authenticationScheme, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TClaimsFactory : class, IClaimsFactory<TIdentity>
            => builder.AddJwtBearer<TIdentity, TIdentityFactory, TClaimsFactory>(authenticationScheme, null, configureIssuingOptions, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TClaimsFactory">The type of claims factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearer<TIdentity, TIdentityFactory, TClaimsFactory>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TClaimsFactory : class, IClaimsFactory<TIdentity>
        {

            builder.Services.Configure(configureIssuingOptions);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IIdentityFactory<TIdentity>, TIdentityFactory>();
            builder.Services.AddTransient<IClaimsFactory<TIdentity>, TClaimsFactory>();
            builder.Services.AddTransient<JsonWebTokenFactory<TIdentity>>();
            builder.Services.AddTransient(provider =>
            {
                var principal = provider.GetService<IHttpContextAccessor>()?.HttpContext?.User;

                if (!principal?.Identity?.IsAuthenticated ?? false)
                {
                    return null;
                }
                return provider.GetService<IIdentityFactory<TIdentity>>().Create(principal);
            });

            return builder.AddJwtBearer(authenticationScheme, displayName, configureValidationOptions);
        }
    }
}
