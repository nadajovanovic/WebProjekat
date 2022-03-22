using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Projekat.Collections
{

    [ApiController]
    [Route("[controller]")]

    public class LigaController: ControllerBase
    {
        public StatistikaContext Context { get; set; }
        
        public LigaController( StatistikaContext context){
            Context = context;
        }
        [Route("DodajLigu/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> DodajLigu(string naziv)
        {
            Liga lige = await Context.Lige.Where(p => p.Naziv.ToLower() == naziv.ToLower()).FirstOrDefaultAsync();
            if (lige!=null)
            {
                return BadRequest("Pozicija vec postoji!");
                
            }
            else {
                Liga liga = new Liga();
                liga.Naziv = naziv;
                if (string.IsNullOrWhiteSpace(liga.Naziv) || liga.Naziv.Length > 50)
                    return BadRequest("Pogrešna vrednost imena!");

                Context.Lige.Add(liga);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je nova pozicija! ID je: {liga.ID}");
            }
        }

        [Route("VratiLige")]
        [HttpGet]
        public async Task<ActionResult> VratiLige()
        {

            
            try
            {
                var lige = await Context.Lige.ToListAsync();

                return Ok
                (
                    lige.Select(p =>
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