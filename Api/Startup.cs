using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Connection;
using Repository.Contracts;
using Repository.Repositories;
using Service.Contracts;
using Service.Services;
using Microsoft.AspNetCore.Authentication;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public readonly byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IClienteRepository, ClienteRepository>();
            services.AddSingleton<IConnectionFactory, DeafultSqlConnectionFactory>();

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


            services.AddSwaggerGen(c =>
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }));
            services.AddSwaggerDocument();
           
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                builder =>
                {
                    builder.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("localhost")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Clientes.API";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwagger(c => {
                c.RouteTemplate = "/api/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api/swagger";
            });
           /* app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/app/WebApiClient";
                settings.DocumentPath = "/specification/specification.json";
            });*/

           
            // Shows UseCors with named policy.

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting()
                .UseCors("AllowAllHeaders")
            .UseAuthentication()
            .UseAuthorization()
               .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            

        }
    }
}
