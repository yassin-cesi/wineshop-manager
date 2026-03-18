using WineshopManagerStarterKit.Models;

namespace WineshopManagerStarterKit.Data;

public static class SupplierSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Suppliers.Any())
        {
            return;
        }

        context.Suppliers.AddRange(
            new Supplier { Name = "Emile " },
            new Supplier { Name = "Margaux" },
            new Supplier { Name = "Barolo " },
            new Supplier { Name = "Housni" }
        );

        context.SaveChanges();
    }
}
