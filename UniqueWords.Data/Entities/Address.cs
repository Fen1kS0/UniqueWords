using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniqueWords.Data.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Word> UniqueWords { get; set; }
    }
}