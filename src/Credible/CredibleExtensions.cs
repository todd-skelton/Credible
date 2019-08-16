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
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        public static IServiceCollection AddCredible<TIdentity, TPayloadFactory>(this IServiceCollection builder, Action<JsonWebTokenFactoryOptions> configureIssuingOptions)
            where TIdentity : class
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
            => builder.RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <param name="services">The service collection for the application.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory>(this IServiceCollection services, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity> =>
            services
            .RegisterValidation<TIdentity, TIdentityFactory>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <param name="services">The service collection for the application.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory>(this IServiceCollection services, string authenticationScheme, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity> =>
            services
            .RegisterValidation<TIdentity, TIdentityFactory>()
            .AddAuthentication(authenticationScheme)
            .AddJwtBearer(authenticationScheme, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <param name="services">The service collection for the application.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory>(this IServiceCollection services, string authenticationScheme, string displayName, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity> =>
            services
            .RegisterValidation<TIdentity, TIdentityFactory>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(authenticationScheme, displayName, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this AuthenticationBuilder builder, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
        {
            builder
                .Services
                .RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions)
                .RegisterValidation<TIdentity, TIdentityFactory>();

            return builder
                .AddJwtBearer(configureValidationOptions);
        }

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
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this AuthenticationBuilder builder, string authenticationScheme, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity>
        {
            builder
                .Services
                .RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions)
                .RegisterValidation<TIdentity, TIdentityFactory>();

            return builder
                .AddJwtBearer(authenticationScheme, configureValidationOptions);
        }

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
            builder
                .Services
                .RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions)
                .RegisterValidation<TIdentity, TIdentityFactory>();

            return builder
                .AddJwtBearer(authenticationScheme, displayName, configureValidationOptions);
        }

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="services">The service collection for the application.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this IServiceCollection services, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity> =>
            services
            .RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions)
            .RegisterValidation<TIdentity, TIdentityFactory>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="services">The service collection for the application.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this IServiceCollection services, string authenticationScheme, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity> =>
            services
            .RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions)
            .RegisterValidation<TIdentity, TIdentityFactory>()
            .AddAuthentication(authenticationScheme)
            .AddJwtBearer(authenticationScheme, configureValidationOptions);

        /// <summary>
        /// Adds Json Web Token Bearer authentication with a token factory for issuing tokens
        /// </summary>
        /// <typeparam name="TIdentity">The type of identity model.</typeparam>
        /// <typeparam name="TIdentityFactory">The type of identity factory.</typeparam>
        /// <typeparam name="TPayloadFactory">The type of payload factory.</typeparam>
        /// <param name="services">The service collection for the application.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureIssuingOptions">Options to configure the token factory.</param>
        /// <param name="configureValidationOptions">Options to configure the authentication handlers.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCredible<TIdentity, TIdentityFactory, TPayloadFactory>(this IServiceCollection services, string authenticationScheme, string displayName, Action<JsonWebTokenFactoryOptions> configureIssuingOptions, Action<JwtBearerOptions> configureValidationOptions)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity>
            where TPayloadFactory : class, IPayloadFactory<TIdentity> =>
            services
            .RegisterIssuing<TIdentity, TPayloadFactory>(configureIssuingOptions)
            .RegisterValidation<TIdentity, TIdentityFactory>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(authenticationScheme, displayName, configureValidationOptions);


        private static IServiceCollection RegisterIssuing<TIdentity, TPayloadFactory>(this IServiceCollection services, Action<JsonWebTokenFactoryOptions> configureIssuingOptions)
            where TIdentity : class
            where TPayloadFactory : class, IPayloadFactory<TIdentity> =>
            services
            .Configure(configureIssuingOptions)
            .AddTransient<IPayloadFactory<TIdentity>, TPayloadFactory>()
            .AddTransient<JsonWebTokenFactory<TIdentity>>();

        private static IServiceCollection RegisterValidation<TIdentity, TIdentityFactory>(this IServiceCollection services)
            where TIdentity : class
            where TIdentityFactory : class, IIdentityFactory<TIdentity> =>
            services
            .AddHttpContextAccessor()
            .AddTransient<IIdentityFactory<TIdentity>, TIdentityFactory>()
            .AddTransient(provider =>
            {
                var principal = provider.GetService<IHttpContextAccessor>()?.HttpContext?.User;

                if (!principal?.Identity?.IsAuthenticated ?? false)
                {
                    return null;
                }
                return provider.GetService<IIdentityFactory<TIdentity>>().Create(principal);
            });
    }
}
