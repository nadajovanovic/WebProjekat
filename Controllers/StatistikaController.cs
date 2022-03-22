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

    public class StatistikaController: ControllerBase
    {
        public StatistikaContext Context { get; set; }
        
        public StatistikaController( StatistikaContext context){
            Context = context;
        }

    }
}