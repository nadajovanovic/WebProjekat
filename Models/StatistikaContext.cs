using Microsoft.EntityFrameworkCore;

namespace Models{

    public class StatistikaContext : DbContext{
        
        public DbSet<Igrac> Igraci {get;set;}
        public DbSet<Liga> Lige {get;set;}
        public DbSet<Pozicija> Pozicije {get;set;}
        public DbSet<Statistika> Statistike {get;set;}
        public DbSet<Tim> Timovi {get;set;}
        public DbSet<Utakmica> Utakmice {get;set;}

        public StatistikaContext(DbContextOptions options) : base(options){
            
        }



    }

}