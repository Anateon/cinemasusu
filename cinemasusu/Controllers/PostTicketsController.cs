using cinemasusu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cinemasusu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostTicketsController : ControllerBase
    {
        [HttpPost]
        public void Post(Dictionary<string, string> a)
        {
            using (var context = new cinemaContext())
            {
                int _sessionsId = int.Parse(a["sessionID"]);
                a.Remove("sessionID");
                foreach (var VARIABLE in a)
                {
                    Debug.WriteLine(VARIABLE.Key + "///" + VARIABLE.Value);

                    context.Tickets.Add(new Ticket()
                    {
                        PlacesId = int.Parse(VARIABLE.Key),
                        SesssionsId = _sessionsId,
                        UserId = 2
                    });
                    context.SaveChanges(); //commit
                }
                
            }
        }
    }
}