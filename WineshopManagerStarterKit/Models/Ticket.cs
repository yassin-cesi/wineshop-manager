using System;
using System.ComponentModel.DataAnnotations;
namespace WineshopManagerStarterKit.Models{

    public class Ticket
   {
        [Required]
	    public int Id { get; set; }
        [Required]
        public DateTime DateVente { get; set; }

    }
}