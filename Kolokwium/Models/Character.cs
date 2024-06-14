﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(120)]
        public string LastName { get; set; }

        public int CurrentWeight { get; set; }

        public int MaxWeight { get; set; }

        public ICollection<Backpack> Backpacks { get; set; } = new HashSet<Backpack>();

        public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();
    }
}