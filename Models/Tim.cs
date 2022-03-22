using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{

    public class Tim
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [MaxLength(50)]
        public string Trener { get; set; }
        
        [MaxLength(50)]
        public string Predsednik { get; set; }

        public List<Igrac> Igraci {get; set;}

        public Liga LigaTima{get;set;}
    
        [JsonIgnore]

        public List<Utakmica> Utakmice {get;set;}
        
    }



}