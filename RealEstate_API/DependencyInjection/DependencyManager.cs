using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;
using RealEstate_API.Repositories.CategoryRepository;
using RealEstate_API.Repositories.Concrete;

namespace RealEstate_API.DependencyInjection
{
    public static class DependencyManager
    {
        public static void ConfigureMyServices(this IServiceCollection services)
        {
            services.AddScoped<Context>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IWhoWerAreDetailRepository, WhoWerAreDetailRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBottomGridRepository, BottomGridRepository>();
            services.AddScoped<IPopularLocationRepository, PopularLocationRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
        }
    }
}
