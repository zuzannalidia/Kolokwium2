using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Weight { get; set; }

        public ICollection<Backpack> Backpacks { get; set; } = new HashSet<Backpack>();
    }
}