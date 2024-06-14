using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models
{
    public class Title
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();
    }
}