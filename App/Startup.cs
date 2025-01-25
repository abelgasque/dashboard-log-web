using App.Controllers;
using App.Entities.EF;
using App.Entities.Settings;
using App.Repositories;
using App.Services;
using App.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            services.AddLogging(configure => configure.AddSerilog());
            services.AddCors();
            services.AddControllers();
            services.AddControllersWithViews();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var proxySettings = Configuration.GetSection("ProxySettings");
            var dbSettingsSection = Configuration.GetSection("DataBaseSettings");
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");

            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<ProxySettings>(proxySettings);
            services.Configure<DataBaseSettings>(dbSettingsSection);
            services.Configure<JwtSettings>(jwtSettingsSection);
            
            var appSettings = appSettingsSection.Get<AppSettings>();
            var dbSettings = dbSettingsSection.Get<DataBaseSettings>();
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.AddDbContext<AppDataContext>(options => options.UseSqlServer(string.Format(dbSettings.ConnectionString, CryptoHelper.Decrypt(dbSettings.Server), CryptoHelper.Decrypt(dbSettings.Database), CryptoHelper.Decrypt(dbSettings.UserId), CryptoHelper.Decrypt(dbSettings.PasswordDb))));                
            
            if (!appSettings.MustRemoveSpaApp)
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });
            }
            
            var key = Encoding.ASCII.GetBytes(CryptoHelper.Decrypt(jwtSettings.Secret));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            if (!appSettings.MustRemoveDocSwagger)
            {                
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
                    c.AddSecurityDefinition(jwtSettings.TokenType, new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Favor Inserir seu token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = jwtSettings.TokenType.ToLower()
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = jwtSettings.TokenType
                                }
                            },
                            new string[]{ }
                        }
                    });
                });
            }
            
            services.AddTransient<DataBaseHelper>();
            services.AddTransient<LogIntegrationRepository>();
            services.AddTransient<LogIntegrationService>();
            services.AddTransient<LogIntegrationController>();            
            services.AddTransient<JwtRepository>();
            services.AddTransient<JwtService>();
            services.AddTransient<TokenController>();
        }

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
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            var appSettingsSection = Configuration.GetSection("AppSettings");            
            var appSettings = appSettingsSection.Get<AppSettings>();

            if (!appSettings.MustRemoveSpaApp)
            {
                app.UseStaticFiles();
                if (!env.IsDevelopment())
                {
                    app.UseSpaStaticFiles();
                }
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            if (!appSettings.MustRemoveDocSwagger)
            {
                // swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "App v1"));
            }

            if (!appSettings.MustRemoveSpaApp)
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }

        }
    }
}