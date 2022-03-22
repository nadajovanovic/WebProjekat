using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Projekat.Collections
{

    [ApiController]
    [Route("[controller]")]

    public class PozicijaController: ControllerBase
    {
        public StatistikaContext Context { get; set; }
        
        public PozicijaController( StatistikaContext context){
            Context = context;
        }

        [Route("DodajPoziciju/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> DodajPoziciju(string naziv)
        {
            Pozicija pozicije = await Context.Pozicije.Where(p => p.Naziv.ToLower() == naziv.ToLower()).FirstOrDefaultAsync();
            if (pozicije!=null)
            {
                return BadRequest("Pozicija vec postoji!");
                
            }
            else {
                Pozicija pozicija = new Pozicija();
                pozicija.Naziv = naziv;
                if (string.IsNullOrWhiteSpace(pozicija.Naziv) || pozicija.Naziv.Length > 50)
                    return BadRequest("Pogrešna vrednost imena!");

                Context.Pozicije.Add(pozicija);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je nova pozicija! ID je: {pozicija.ID}");
            }
        }

        [Route("VratiPozicije")]
        [HttpGet]
        public async Task<ActionResult> VratiPozicije()
        {
            try
            {
                var pozicije = await Context.Pozicije.ToListAsync();

                return Ok
                (
                    pozicije.Select(p =>
                    new
                    {
                        naziv = p.Naziv,
                        id = p.ID
                    }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}