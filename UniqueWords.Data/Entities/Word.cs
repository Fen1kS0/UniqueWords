using System;
using System.ComponentModel.DataAnnotations;

namespace UniqueWords.Data.Entities
{
    public class Word
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int Count { get; set; }

        [Required]
        public Address Address { get; set; }
    }
}