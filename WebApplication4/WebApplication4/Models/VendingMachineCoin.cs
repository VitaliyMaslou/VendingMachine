using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace WebApplication4.Models
{
    public partial class VendingMachineCoin
    {
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public int CoinsId { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual Coin Coins { get; set; }
        [JsonIgnore]
        public virtual VendingMachine VendingMachine { get; set; }
    }
}
