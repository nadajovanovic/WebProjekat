using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{


    public class Utakmica{


        [Key]
        public int ID { get; set; }
        
        public Liga Liga {get;set;}

        [Required]
        public List <Tim> Timovi {get;set;}
        
        public string Info { get; set; }

    }
}