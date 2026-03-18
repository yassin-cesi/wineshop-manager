using WineshopManagerStarterKit.Models;

namespace WineshopManagerStarterKit.Data;

public static class OrderSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Orders.Any())
        {
            return;
        }

        context.Orders.AddRange(
            new Order { Supplier = 0,  Validated = true, Received = true }
        );

        context.SaveChanges();
    }
}
