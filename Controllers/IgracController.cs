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

    public class IgracController: ControllerBase
    {
        public StatistikaContext Context { get; set; }
        
        public IgracController( StatistikaContext context){
            Context = context;
        }

        [Route("DodajIgraca/{jmbg}/{ime}/{prezime}/{godina}/{pozicijaID}/{timID}")]
        [HttpPost]
        public async Task<ActionResult> DodajIgraca(long jmbg, string ime, string prezime, int godina, int pozicijaID, int timID)
        {
            Igrac igraci = await Context.Igraci.Where(i => i.JMBG == jmbg).FirstOrDefaultAsync();
            if (igraci!=null)
            {
                return BadRequest("Vec postoji igrac sa istim jmbg-om!");
                
            }
            else {
                Igrac igrac = new Igrac();
                igrac.JMBG = jmbg;
                igrac.Ime = ime;
                igrac.Prezime = prezime;
                igrac.Godina = godina;
                Tim OdTim = await Context.Timovi.FirstOrDefaultAsync(t => t.ID == timID);
                igrac.TimIgraca = OdTim;
                Pozicija OdPozicija = await Context.Pozicije.FirstOrDefaultAsync(p => p.ID == pozicijaID);
                igrac.PozicijaIgraca = OdPozicija;
                if (igrac.JMBG < 1000000000000 || igrac.JMBG > 9999999999999)
                    return BadRequest("Pogrešna vrednost jmbg-a!");
                if (string.IsNullOrWhiteSpace(igrac.Ime) || igrac.Ime.Length > 20)
                    return BadRequest("Pogrešna vrednost imena!");
                if (string.IsNullOrWhiteSpace(igrac.Prezime) || igrac.Prezime.Length > 20)
                    return BadRequest("Pogrešna vrednost prezimena!");
                if (igrac.Godina > 65 || igrac.Godina < 15)
                    return BadRequest("Pogrešna vrednost godina starosti!");

                Context.Igraci.Add(igrac);
                await Context.SaveChangesAsync();
                return Ok($"Igrac je dodat! ID je: {igrac.ID}");
            }
        }

         
        [Route("IzbrisiIgraca/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisatiIgraca(int id)
        {
            try
            {
                var igrac = Context.Igraci.Where(i => i.ID == id).FirstOrDefault();
                long jmbg = igrac.JMBG;
                Context.Igraci.Remove(igrac);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan igrac sa jmbg-om: {jmbg}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("VratiIgrace/{TimID}")]
        [HttpGet]
        public async Task<ActionResult> VratiIgrace(int TimID)
        {
            try
            {
                return Ok(await Context.Igraci
                    .Include(i => i.PozicijaIgraca)
                    .Include(i => i.TimIgraca)
                    .Where(i => i.TimIgraca.ID == TimID)
                    .Select( i =>
                    new{
                        ID = i.ID,
                        JMBG = i.JMBG,
                        Ime = i.Ime,
                        Prezime = i.Prezime,
                        Godina = i.Godina,
                        IdPoz = i.PozicijaIgraca.ID,
                        Poz = i.PozicijaIgraca.Naziv,
                        tim = i.TimIgraca.Naziv
                    }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("AzurirajIgraca/{ID}/{ime}/{prezime}/{godina}/{pozicijaID}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajIgraca(int ID, string ime, string prezime, int godina, int pozicijaID)
        {

            try
            {
                var igrac = await Context.Igraci.FindAsync(ID);
                if (igrac == null)
                    return StatusCode(404);
                else{

                    Pozicija OdPozicija = await Context.Pozicije.FirstOrDefaultAsync(p => p.ID == pozicijaID);
                    igrac.PozicijaIgraca = OdPozicija;
                    if (string.IsNullOrWhiteSpace(ime) || ime.Length > 20)
                        return BadRequest("Pogrešna vrednost imena!");
                    if (string.IsNullOrWhiteSpace(prezime) || prezime.Length > 20)
                        return BadRequest("Pogrešna vrednost prezimena!");
                    if (godina > 65 || godina < 15)
                        return BadRequest("Pogrešna vrednost godina starosti!");
                    if(igrac.Ime != ime) igrac.Ime = ime;
                    if(igrac.Prezime != prezime) igrac.Prezime = prezime;
                    if(igrac.Godina != godina) igrac.Godina = godina;

                    Context.Update(igrac);
                    
                    await Context.SaveChangesAsync();

                    return Ok("Uspesno azuriran igrac!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}