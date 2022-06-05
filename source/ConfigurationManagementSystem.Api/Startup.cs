using ConfigurationManagementSystem.Api.Extensions.ServiceCollection;
using ConfigurationManagementSystem.Api.Middleware;
using ConfigurationManagementSystem.ServicesConfiguring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.ConfigureExceptionHandling()
                .ConfigureLogging(Configuration)
                .ConfigureApplicationServices(Configuration)
                .ConfigureCors(Configuration)
                .ConfigureAuthentication(Configuration)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConfigurationService.Api", Version = "v1" });
                })
                .AddControllers()
                .ConfigureApiBehavior();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsEnvironment("production"))
            {
                // Needs to pass headers to the reverse proxy.
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConfigurationService.Api v1"));

            app.UseHttpLogging()
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseRouting()
                .UseCors()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
