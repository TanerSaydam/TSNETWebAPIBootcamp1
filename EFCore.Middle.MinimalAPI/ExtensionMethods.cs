using EFCore.Middle.MinimalAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Middle.MinimalAPI;

public static class ExtensionMethods
{
    public static void MigrateDatabase(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var context = provider.GetRequiredService<ApplicationDbContext>(); //=> DI Yöntemi
        context.Database.Migrate();
    }
}
