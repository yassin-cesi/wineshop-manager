namespace WineshopManagerStarterKit.Models;

public class Order
{
		public int Id { get; set; }
		public int SupplierId { get; set; }
		public Supplier Supplier { get; set; }
		public DateTime? DateOrder { get; set; } = null;
		public DateTime? DateDelivery { get; set; } = null;
		public string Street { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public bool Validated { get; set; }
		public bool Received { get; set; }
}
