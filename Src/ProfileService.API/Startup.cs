using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProfileService.Application.Profiles.Commands;
using ProfileService.Domain.Profiles;
using ProfileService.Persistence;
using ProfileService.Persistence.Profiles;

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

            services.AddMediatR(typeof(CreateProfile).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
