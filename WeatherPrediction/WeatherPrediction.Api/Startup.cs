using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WeatherPrediction.Crosscutting.DI;
using WeatherPrediction.Crosscutting.Mapping;
using WeatherPrediction.Infrastructure.Helpers;
using System.Globalization;
using System.Text;
using WeatherPrediction.Infrastructure.Extensions;

namespace WeatherPrediction.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            EnvironmentNameHelper.Value = Configuration.GetSection("EnvironmentName").Value;

            services.AddControllers();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 5000;
                x.MultipartBodyLengthLimit = 100_000_000;
                x.MultipartHeadersLengthLimit = 100_000_000;
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowFrontEnd", options => options.WithOrigins(Configuration["AppSettings:WebUrl"] + "/"));
            });

            services.AddRazorPages();

            services.AddDistributedMemoryCache();

            services.AddDependencyInjection();
            services.AddAutoMapper();

            services.AddEndpointsApiExplorer();
            services.AddSwagger("WeatherPrediction Api", "v1");

            ConfigureAuthentication(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature.Error;
                        var result = JsonConvert.SerializeObject(new { error = exception.Message });
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(result);
                    });
            });

            app.UseCors(options => options.WithOrigins(Configuration["AppSettings:WebUrl"]).AllowAnyMethod().AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:SecretToken"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = Configuration["AppSettings:AudienceApi"],
                        ValidIssuer = Configuration["AppSettings:Issuer"]
                    };
                });
        }
    }
}