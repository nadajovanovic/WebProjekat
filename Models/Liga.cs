using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models{

    public class Liga{

        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Naziv { get; set; }

        public List<Tim> Timovi {get;set;}

        public List<Utakmica> Utakmice {get;set;}
        

    }
}