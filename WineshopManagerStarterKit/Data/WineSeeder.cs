using System;
using System.Linq;
using WineshopManagerStarterKit.Models;

namespace WineshopManagerStarterKit.Data;

public static class WineSeeder
{
    public static void Seed(AppDbContext context)
    {
        // S'assure que les TypeVins existent avant de créer des Wines
        TypeVinSeeder.Seed(context);

        if (context.Wines.Any())
        {
            return;
        }

        var rouge = context.TypeVins.FirstOrDefault(t => t.Name == "Rouge");
        var blanc = context.TypeVins.FirstOrDefault(t => t.Name == "Blanc");
        var rose = context.TypeVins.FirstOrDefault(t => t.Name == "Rosé");

        if (rouge == null || blanc == null || rose == null)
        {
            throw new InvalidOperationException("Les TypeVins attendus (Rouge, Blanc, Rosé) doivent exister avant de seed les Wines.");
        }

        context.Wines.AddRange(
            new Wine
            {
                Name = "Château Margaux",
                Quantite = 10,
                Montant = 250.0f,
                TauxTaxe = 20.0f,
                Seuil = 2,
                TypeVinId = rouge.Id
            },
            new Wine
            {
                Name = "Barolo Riserva",
                Quantite = 8,
                Montant = 180.0f,
                TauxTaxe = 20.0f,
                Seuil = 1,
                TypeVinId = rouge.Id
            },
            new Wine
            {
                Name = "Chablis Premier Cru",
                Quantite = 12,
                Montant = 75.0f,
                TauxTaxe = 20.0f,
                Seuil = 3,
                TypeVinId = blanc.Id
            },
            new Wine
            {
                Name = "Rioja Gran Reserva",
                Quantite = 6,
                Montant = 95.0f,
                TauxTaxe = 20.0f,
                Seuil = 2,
                TypeVinId = rouge.Id
            },
            new Wine
            {
                Name = "Napa Valley Cabernet Sauvignon",
                Quantite = 5,
                Montant = 210.0f,
                TauxTaxe = 20.0f,
                Seuil = 1,
                TypeVinId = rouge.Id
            }
        );

        context.SaveChanges();
    }
}