using ConfigurationManagementSystem.Api.Extensions;
using ConfigurationManagementSystem.Api.Filters;
using ConfigurationManagementSystem.ServicesConfiguring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;

namespace ConfigurationManagementSystem.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLogging(Configuration);
            services.ConfigureApplicationServices(Configuration);
            services.ConfigureCors(Configuration);
            services.ConfigureAuthentication(Configuration);
            
            services.AddControllers(options => options.Filters.Add(new ApplicationExceptionFilter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConfigurationService.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (_env.IsProduction())
            {
                // Needs to pass headers to the reverse proxy.
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConfigurationService.Api v1"));

            ChangeToken.OnChange(Configuration.GetReloadToken, lifetime.StopApplication);

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
