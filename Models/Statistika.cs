using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{


    public class Statistika{

        [Key]
        public int ID { get; set; }

        [Required]
        public Tim Tim {get;set;}

        [Required]
        public Utakmica Utakmica {get;set;}
        public int Golovi { get; set; }
        public int UkupnoSuteva { get; set; }
        public int SuteviUOkrvi { get; set; }
        public int SuteviVanOkvrira { get; set; }
        public int SlobodniUdarci { get; set; }
        public int Korneri { get; set; }
        public int Ofsajdi { get; set; }
        public int OdbraneGolmana { get; set; }

        public int Prekrsaji { get; set; }

        public int ZutiKartoni { get; set; }

        public int CrveniKartoni { get; set; }   
         

    }
}