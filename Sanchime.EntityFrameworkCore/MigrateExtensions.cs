using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EntityFrameworkCore;

public static class MigrateExtensions
{
    public static async ValueTask<IServiceProvider> MigrateDatabaseAsync<TDbContext>(this IServiceProvider provider)
        where TDbContext : DbContext
    {
        await using var scope = provider.CreateAsyncScope();

        using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        await context.Database.MigrateAsync();

        return provider;
    }

    public static async ValueTask<IServiceProvider> MigrateDatabaseAsync<TDbContext, TSeeder>(this IServiceProvider provider)
        where TDbContext : DbContext
        where TSeeder: IDataSeeder
    {
        await using var scope = provider.CreateAsyncScope();

        using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();

        await context.Database.MigrateAsync();
        await seeder.Initialize();

        return provider;
    }
}
