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

    public class TimController: ControllerBase
    {
        public StatistikaContext Context { get; set; }
        
        public TimController( StatistikaContext context){
            Context = context;
        }


        [Route("DodajTim/{naziv}/{trener}/{predsednik}/{ligaid}")]
        [HttpPost]
        public async Task<ActionResult> DodajTim(string naziv, string trener, string predsednik, int ligaid)
        {
            Tim timovi = await Context.Timovi.Where(t=> t.Naziv.ToUpper() == naziv.ToUpper()).FirstOrDefaultAsync();
            if (timovi!=null)
            {
                return BadRequest("Vec postoji tim sa istim nazivom!");
                
            }
            else {
                Tim tim = new Tim();
                tim.Naziv = naziv;
                tim.Predsednik = predsednik;
                tim.Trener= trener;
                Liga OdLiga = await Context.Lige.FirstOrDefaultAsync(l => l.ID == ligaid);
                tim.LigaTima = OdLiga;
        
                if (string.IsNullOrWhiteSpace(tim.Naziv) || tim.Naziv.Length > 50)
                    return BadRequest("Pogrešna vrednost naziva!");
                if (string.IsNullOrWhiteSpace(tim.Predsednik) || tim.Predsednik.Length > 50)
                    return BadRequest("Pogrešna vrednost predsednika!");
                if (string.IsNullOrWhiteSpace(tim.Trener) || tim.Trener.Length > 50)
                    return BadRequest("Pogrešna vrednost trenera!");
                
                Context.Timovi.Add(tim);
                await Context.SaveChangesAsync();
                return Ok($"Tim je dodat! ID je: {tim.ID}");
            }
        }

         
        [Route("IzbrisiTim/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiTim(int id)
        {
            try
            {
                var tim = Context.Timovi.Where(t => t.ID == id).FirstOrDefault();
                string naziv = tim.Naziv;
                Context.Timovi.Remove(tim);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan tim: {naziv}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("VratiTim/{ligaID}")]
        [HttpGet]
        public async Task<ActionResult> VratiTim(int ligaID)
        {
            try
            {
                return Ok(await Context.Timovi
                    .Where(t => t.LigaTima.ID == ligaID)
                    .Select(t =>
                    new{
                        ID = t.ID,
                        Naziv = t.Naziv,
                        Predsednik = t.Predsednik,
                        Trener = t.Trener
                    }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); 
            }
        }

        [Route("AzurirajTim/{ID}/{naziv}/{predsednik}/{trener}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajTim(int ID, string naziv, string predsednik, string trener)
        {

            try
            {
                var tim = await Context.Timovi.FindAsync(ID);
                if (tim == null)
                    return StatusCode(404);
                else{                  
                    if (string.IsNullOrWhiteSpace(naziv) || naziv.Length > 50)
                        return BadRequest("Pogrešna vrednost naziva!");
                    if (string.IsNullOrWhiteSpace(predsednik) || predsednik.Length > 50)
                       return BadRequest("Pogrešna vrednost predsednika!");
                    if (string.IsNullOrWhiteSpace(trener) || trener.Length > 50)
                    return BadRequest("Pogrešna vrednost trenera!");
                    if(tim.Naziv != naziv) tim.Naziv = naziv;
                    if(tim.Trener != predsednik) tim.Trener = predsednik;
                    if(tim.Predsednik != trener) tim.Predsednik = trener;

                    Context.Update(tim);
                    
                    await Context.SaveChangesAsync();

                    return Ok("Uspesno azuriran tim!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
 


    }
}

