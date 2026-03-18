using System;
namespace WineshopManagerStarterKit.Models {

public class Client
{
	public int Id { get; set; }
	public string Nom { get; set; }
	public string Email { get; set; }
	public int NumRue { get; set; }
	public string CodePostal { get; set; }
	public string Ville { get; set; }
	public string Telephone { get; set; }

}
}