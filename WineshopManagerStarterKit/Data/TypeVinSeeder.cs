using System.Linq;
using WineshopManagerStarterKit.Models;

namespace WineshopManagerStarterKit.Data;

public static class TypeVinSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.TypeVins.Any())
        {
            return;
        }

        context.TypeVins.AddRange(
            new TypeVin { Name = "Rouge" },
            new TypeVin { Name = "Blanc" },
            new TypeVin { Name = "Rosé" }
        );

        context.SaveChanges();
    }
}