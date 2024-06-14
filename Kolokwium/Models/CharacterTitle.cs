using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models
{
    public class CharacterTitle
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Title")]
        public int TitleId { get; set; }
        public Title Title { get; set; }

        public DateTime AcquiredAt { get; set; }
    }
}