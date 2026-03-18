namespace WineshopManagerStarterKit.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StreetNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Siret { get; set; }
}
