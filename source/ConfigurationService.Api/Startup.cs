using ConfigurationService.Api.Extensions;
using ConfigurationService.Api.Filters;
using ConfigurationService.ServiceCollectionConfiguring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;

namespace ConfigurationService.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
            Configuration = GetConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLogging(Configuration);
            services.ConfigureApplicationServices();
            services.ConfigureCors(Configuration);
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConfigurationService.Api v1"));
            }
            else
            {
                // needs to pass headers to the reverse proxy.
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            ChangeToken.OnChange(Configuration.GetReloadToken, lifetime.StopApplication);

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IConfiguration GetConfiguration()
        {
            var conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", true, true)
                .Build();
            return conf;
        }
    }
}
