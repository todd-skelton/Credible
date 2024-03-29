﻿using Credible;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WebApiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("My super secret key."));

            services.AddCredible<UserIdentity, UserIdentityFactory, PayloadFactory>(
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
