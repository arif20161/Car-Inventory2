using System;
using System.Collections.Generic;

namespace CI.Business.Models
{
    public partial class Car
    {
        public System.Guid CarId { get; set; }
        public System.Guid UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool New { get; set; }
        public virtual User User { get; set; }
    }
}
