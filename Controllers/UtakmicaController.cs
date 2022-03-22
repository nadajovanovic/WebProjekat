using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Projekat.Collections
{

    [ApiController]
    [Route("[controller]")]

    public class UtakmicaController: ControllerBase
    {
        public StatistikaContext Context { get; set; }
        
        public UtakmicaController( StatistikaContext context){
            Context = context;
        }

        [Route("DodajUtakmicu/{info}/{ligaID}/{domacinID}/{gostID}")]
        [HttpPost]
        public async Task<ActionResult> DodajUtakmicu(int ligaID, string info, int domacinID, int gostID)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(info)){
                    return BadRequest("Niste uneli dodatne informacije!");
                } 
                Utakmica utakmica = new Utakmica();
                utakmica.Info = info;
                utakmica.Liga = await Context.Lige.FindAsync(ligaID);
                
                if(utakmica.Timovi == null)
                    utakmica.Timovi = new List<Tim>();

                Tim domacin = await Context.Timovi.FindAsync(domacinID);

                if(domacin != null ){
                    utakmica.Timovi.Add(domacin);
                    if(domacin.Utakmice == null) domacin.Utakmice = new List<Utakmica>();
                    domacin.Utakmice.Add(utakmica);
                }
                Tim gost = await Context.Timovi.FindAsync(gostID);

                if(gost != null ){
                    utakmica.Timovi.Add(gost);
                    if(gost.Utakmice == null) gost.Utakmice = new List<Utakmica>();
                    gost.Utakmice.Add(utakmica);
                }
            
                Context.Utakmice.Add(utakmica);
                
                await Context.SaveChangesAsync();
                return Ok($"Utakmica je dodata! ID je: {utakmica.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("UtakmiceTimovi/{TimID}")]
        [HttpGet]
        public async Task<ActionResult> UtakmiceTimovi(int TimID)
        {
            try
            {
                
                var lista = await Context.Timovi
                .Where(p => p.ID == TimID)
                .Include( p => p.Utakmice)
                .ThenInclude( u => u.Timovi)
                .Select( c => c.Utakmice)
                .FirstOrDefaultAsync();
            
                if (lista == null)
                {
                    return BadRequest("Nije pronadjena nijedna utakmica");
                }

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

   
}