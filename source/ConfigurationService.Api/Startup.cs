using System;
using ConfigurationService.Api.Filters;
using ConfigurationService.Application;
using ConfigurationService.Application.Sources;
using ConfigurationService.Application.Sources.GitHub;
using ConfigurationService.Application.Sources.GitHub.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfigurationService.Api
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
            var section = Configuration.GetSection(GitHubApiOptions.Path);
            services.Configure<GitHubApiOptions>(section);

            services.AddHttpClient<DefaultHttpClient>(builder =>
            {
                var cfg = section.Get<GitHubApiOptions>();
                builder.BaseAddress = new Uri($"{cfg.Uri}/repos/{cfg.UserName}/");
                builder.DefaultRequestHeaders.Add("Authorization", $"token {cfg.PersonalToken}");
                builder.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                builder.DefaultRequestHeaders.Add("User-Agent", "Configuration-Service");
            });

            services.AddScoped<ISourceApi, GitHubApi>();

            services.AddControllers(options =>
                options.Filters.Add(new HttpExceptionFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
