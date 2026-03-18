using System.Collections.Generic;

namespace WineshopManagerStarterKit.Models;

public class TypeVin
{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Wine> Wines { get; set; } = new();
}
