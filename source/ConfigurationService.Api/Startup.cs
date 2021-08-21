using ConfigurationService.Api.Extensions;
using ConfigurationService.Api.Filters;
using ConfigurationService.ServiceCollectionConfiguring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            Configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json", false, true)
                     .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", true, true)
                     .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationServices();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        var hosts = Configuration.GetAllowedHosts();
                        builder
                            .WithOrigins(hosts)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddControllers(options => options.Filters.Add(new HttpExceptionFilter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConfigurationService.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
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

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
