using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    
    public class Pozicija{

        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Naziv { get; set; }

    }

    
}