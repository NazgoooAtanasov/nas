using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nas.Data;
using Nas.Models;
using Nas.Services;

namespace Nas.API
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
            var data = this.Configuration["DatabaseSettings:ConnectionString"];
            services.AddDbContext<NasDbContext>(
                opts => opts.UseNpgsql(data));

            services.AddControllers().AddFluentValidation();

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
            });

            services.AddTransient<IValidator<CreateUriModel>, CreateUriModelValidator>();
            services.AddTransient<IValidator<RedirectUriModel>, RedirectUriModelValidator>();
            services.AddScoped<IUriService, UriService>();
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

            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}