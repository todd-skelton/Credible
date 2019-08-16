using Credible;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("My super secret key."));

            services.AddAuthentication("Bearer")
                .AddCredible<UserIdentity, UserIdentityFactory, PayloadFactory>("Bearer",
                    issueOptions =>
                    {
                        issueOptions.Audience = "WebApiSample";
                        issueOptions.Issuer = "WebApiSample";
                        issueOptions.Expiration = TimeSpan.FromMinutes(30);
                        issueOptions.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
