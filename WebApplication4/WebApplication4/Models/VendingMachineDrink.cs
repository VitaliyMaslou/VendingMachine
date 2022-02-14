using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace WebApplication4.Models
{
    public partial class VendingMachineDrink
    {
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public int DrinksId { get; set; }
        public int Count { get; set; }

        [JsonIgnore]
        public virtual Drink Drinks { get; set; }
        [JsonIgnore]
        public virtual VendingMachine VendingMachine { get; set; }
    }
}
