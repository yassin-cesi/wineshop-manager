namespace WineshopManagerStarterKit.Models;

public class Supplier
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string StreetNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public required string Siret { get; set; }
}
