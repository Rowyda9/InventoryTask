using BackCore.Base;
using BackCore.BLL;
using BackCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackCore.API.Extenstions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped(typeof(IRepositry<>), typeof(Repositry<>)); 
            services.AddScoped(typeof(IStatusService), typeof(StatusService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
        }
    }
}
