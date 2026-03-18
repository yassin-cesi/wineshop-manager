namespace WineshopManagerStarterKit.Models;

using System.ComponentModel.DataAnnotations;


public class Wine
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "entrez le nom du vin")]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "La quantité est obligatoire")]
    [Range(0, int.MaxValue, ErrorMessage = "La quantité doit être un nombre positif")]
    public int Quantite { get; set; }


    [Required(ErrorMessage = "Le taux de la taxe est obligatoire")]
    [Range(0, 100, ErrorMessage = "Le taux de la taxe doit être entre 0 et 100")]
    public float TauxTaxe { get; set; }


    [Required(ErrorMessage = "Le seuil est obligatoire")]
    [Range(0, int.MaxValue, ErrorMessage = "Le seuil doit être un nombre positif")]
    public int Seuil { get; set; }


    [Required(ErrorMessage = "Le type de vin est à préciser")]
    [Range(1, int.MaxValue, ErrorMessage = "Sélectionnez un type de vin valide")]
    public int TypeVinId { get; set; }


    public TypeVin TypeVin { get; set; }

    public float Montant { get; set; }
}


