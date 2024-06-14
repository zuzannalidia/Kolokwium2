using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models
{
    public class Backpack
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Amount { get; set; }
    }
}