using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniqueWords.Data.Entities
{
    public class Site
    {
        public Guid Id { get; set; }

        [Required]
        public string Uri { get; set; }

        public ICollection<Word> UniqueWords { get; set; }
    }
}