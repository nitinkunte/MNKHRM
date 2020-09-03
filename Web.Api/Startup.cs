using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
using Web.Api.Data;
using Web.Api.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

namespace Web.Api
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("sqliteConnection")));

            services.AddOptions();


            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                    .AddAzureADBearer(options =>

                    {
                        options.Instance = "https://login.microsoftonline.com/";
                        options.ClientId = "api://mnkhrm";
                        options.Domain = "mnkinfotech.onmicrosoft.com";
                        options.TenantId = "ffb79e93-1ae2-4f94-9ff3-3553938a5d00";
                        var x = options;
                    }

                    //Configuration.Bind("AzureAd", options)
                    );
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });


            #region Jwt Section

            ///TODO - remove this before production
            IdentityModelEventSource.ShowPII = true;

            //IConfigurationSection jwtAuthenticationSection = Configuration.GetSection("JwtAuthentication");

            //AddJwtAuthentication(services, jwtAuthenticationSection);
            #endregion Jwt Section



            // configure DI for application services
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddControllers();

            #region Swagger
            AddSwagger(services);
            #endregion Swagger

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //            app.UseCors("CorsPolicy");
            app.UseCors(options =>
                    options.WithOrigins("https://localhost:32516")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MNK HRM WebApi");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddJwtAuthentication(IServiceCollection services, IConfigurationSection jwtAuthenticationSection)
        {
            // configure jwt authentication
            services.Configure<JwtAuthenticationSetting>(jwtAuthenticationSection);

            var jwtAuthenticationSettings = jwtAuthenticationSection.Get<JwtAuthenticationSetting>();

            services.AddAuthentication(y =>
            {
                y.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                y.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var prin = context.Principal;
                            return Task.CompletedTask;
                        },

                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            return Task.CompletedTask;
                        },

                        //OnTokenValidated = context =>
                        //{
                        //    var sessionService = context.HttpContext.RequestServices.GetRequiredService<DataService.Services.ISessionService>();

                        //    var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                        //    var sessionId = SessionHelper.GetSessionIdFromJwt(authorizationHeader);
                        //    var userName = SessionHelper.GetUserNameFromJwt(authorizationHeader);
                        //    bool isValidSession = SessionHelper.IsSessionValid(sessionId, sessionService, userName);
                        //    if (isValidSession == false)
                        //    {
                        //        var errorMsg = "Invalid Session or Session has expired. Please login again.";
                        //        // return unauthorized if user no longer exists
                        //        context.Fail(errorMsg);
                        //        context.Response.Headers.Add("Error-Details", errorMsg);
                        //    }
                        //    return Task.CompletedTask;
                        //},

                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };

                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtAuthenticationSettings.SymmetricSecurityKey,
                        ValidateIssuer = true,
                        ValidIssuer = jwtAuthenticationSettings.ValidIssuer,
                        ValidateAudience = false,
                        ValidAudience = jwtAuthenticationSettings.ValidAudience, //this is redudant as we are not validating audience
                    };
                });

        }

        //private static void AddSwaggerNew(IServiceCollection services, AuthenticationOptions authenticationOptions)
        //{
        //    services.AddSwaggerGen(o =>
        //    {
        //        // Setup our document's basic info
        //        o.SwaggerDoc("v1", new OpenApiInfo
        //        {
        //            Title = "MNK.HRM API",
        //            Version = "1.0"
        //        });

        //        // Define that the API requires OAuth 2 tokens
        //        o.AddSecurityDefinition("aad-jwt", new OpenApiSecurityScheme
        //        {
        //            Type = SecuritySchemeType.OAuth2,
        //            Flows = new OpenApiOAuthFlows
        //            {
        //                // We only define implicit though the UI does support authorization code, client credentials and password grants
        //                // We don't use authorization code here because it requires a client secret, which makes this sample more complicated by introducing secret management
        //                // Client credentials could work, but not when the UI client id == API client id. We'd need a separate registration and granting app permissions to that. And also needs a secret.
        //                // Password grant we don't use because... you shouldn't be using it.
        //                Implicit = new OpenApiOAuthFlow
        //                {
        //                    AuthorizationUrl = new Uri(authenticationOptions.AuthorizationUrl),
        //                    Scopes = DelegatedPermissions.All.ToDictionary(p => $"{authenticationOptions.ApplicationIdUri}/{p}")
        //                }
        //            }
        //        });

        //        // Add security requirements to operations based on [Authorize] attributes
        //        o.OperationFilter<OAuthSecurityRequirementOperationFilter>();

        //        // Include XML comments to documentation
        //        //string xmlDocFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Joonasw.AadTestingDemo.API.xml");
        //        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //        o.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        //        o.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
        //    });
        //}

        private static void AddSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MNK HRM WebApi",
                    Version = "v1.0"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
            });
        }

    }


}
