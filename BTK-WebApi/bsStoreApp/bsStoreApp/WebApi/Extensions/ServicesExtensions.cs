using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        //buradaki this ile şu şekiled bir bu static methodu çağırabiliriz => myClass.ConfigureSqlOptions(), serviceCollection için bir parametre vermemiz gerekmiyor
        //çünkü bu methodu sadece IServiceCollection tipindekiler çağırabilir.
        //bu şekil this kullanımı "Extension Method" olarak geçmektedir.
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
            );
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
