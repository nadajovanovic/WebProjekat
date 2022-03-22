using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Igrac
    {   
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(1000000000000, 9999999999999)]
        public long JMBG { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Prezime { get; set; }
        
        [Required]
        [Range(15, 65)]
        public int Godina { get; set; }

        public Pozicija PozicijaIgraca { get; set; }

        public Tim TimIgraca{ get;set;}
    }
}