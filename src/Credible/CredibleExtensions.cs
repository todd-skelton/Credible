using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Credible
{
    /// <summary>
    /// Extensions to use Credible
    /// </summary>
    public static class CredibleExtensions
    {
        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        public static IServiceCollection AddCredible<TIdentity, TPayloadFactory>(this IServiceCollection builder, Action<JsonWebTokenFactoryOptions> configureIssuingOptions)
            where TIdentity : class
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
            => builder.RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with only validation
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory>(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            => builder.AddCredible<TIdentity, TIdentityFactory>(JwtBearerDefaults.AuthenticationScheme, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with only validation
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory>(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            => builder.AddCredible<TIdentity, TIdentityFactory>(authenticationScheme, null, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with only validation
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            => builder.RegisterValidation<TIdentity, TIdentityFactory>(authenticationScheme, displayName, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this AuthenticationBuilder builder, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
            => builder.AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(JwtBearerDefaults.AuthenticationScheme, configureIssuingOptions, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this AuthenticationBuilder builder, string authenticationScheme, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
            => builder.AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(authenticationScheme, null, configureIssuingOptions, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
        {
            builder.Services.RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions);
            return builder.RegisterValidation<TIdentity, TIdentityFactory>(authenticationScheme, displayName, configureValidationOptions);
        }

        private static IServiceCollection RegisterIssuing<TIdentity, TPayloadFactory>(this IServiceCollection services, Action<JsonWebTokenFactoryOptions> configureIssuingOptions)
            where TIdentity : class
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
            => services.Configure(configureIssuingOptions)
                .AddTransient<IPayloadFactory<TIdentity>, TPayloadFactory>()
                .AddTransient<JsonWebTokenFactory<TIdentity>>();

        private static AuthenticationBuilder RegisterValidation<TIdentity, TIdentityFactory>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IIdentityFactory<TIdentity>, TIdentityFactory>();
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
