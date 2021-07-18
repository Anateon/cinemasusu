using System;
using System.Collections.Generic;

#nullable disable

namespace cinemasusu.Models
{
    public partial class AddedValue
    {
        public int AddValueId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
