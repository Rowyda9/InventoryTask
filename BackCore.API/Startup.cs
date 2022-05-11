
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BackCore.Data;
using BackCore.API.Seed;
using BackCore.API.Extenstions;

namespace CodeAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddControllers();
            services.AddCorsExtension();
            services.AddIdentityInfrastructure(Configuration);
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddResponseCompression();
            services.AddRepositoryExtension();
            services.AddMvc();
            services.AddSwaggerExtension();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();

            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseStaticFilesExtension();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedData.Initalize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()
                .ServiceProvider);

        }
    }
}
