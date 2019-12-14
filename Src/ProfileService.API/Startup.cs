using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileService.Application.Profiles.Commands;
using ProfileService.Domain.Profiles;
using ProfileService.Persistence;
using ProfileService.Persistence.Profiles;
using FluentValidation.AspNetCore;
using static ProfileService.Application.Profiles.Commands.CreateProfile;

namespace ProfileService.API
{
    public class Startup
    {
        private const string ConnectionString = "ConnectionString";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>
            {
                var cnn = Configuration[ConnectionString];
                if (string.IsNullOrEmpty(cnn))
                {
                    opt.UseInMemoryDatabase(databaseName: "Profiles");
                }
                else
                {
                    opt.UseSqlServer(cnn);
                }
            });
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProfileValidator>());
            services.AddMediatR(typeof(CreateProfile).GetTypeInfo().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v0.1",
                    Title = "Profile Service API",
                    Description = "Profile Service API to take care of the customers' profile"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Profile Service API Documentation";
                c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "Profile Service API v0.1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
