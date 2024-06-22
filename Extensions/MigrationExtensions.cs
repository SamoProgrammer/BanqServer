using Microsoft.EntityFrameworkCore;
using Banq.Database;

namespace Banq.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using DatabaseContext dbContext =
            scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        dbContext.Database.Migrate();
    }
}