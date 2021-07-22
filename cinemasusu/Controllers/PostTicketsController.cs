using cinemasusu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cinemasusu.Authorization;
using Microsoft.EntityFrameworkCore;
using cinemasusu.Models;

namespace cinemasusu.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostTicketsController : ControllerBase
    {
        [HttpPost]
        public IEnumerable<object> Post(Dictionary<string, string> a)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (var context = new DataContext())
            {
                int _sessionsId = int.Parse(a["sessionID"]);
                a.Remove("sessionID");
                foreach (var VARIABLE in a)
                {
                    Ticket tmpTicket = new Ticket()
                    {
                        PlacesId = int.Parse(VARIABLE.Key),
                        SesssionsId = _sessionsId,
                        UserId = ((User)this.HttpContext.Items["User"]).Id
                    };
                    context.Tickets.Add(tmpTicket);
                    tickets.Add(tmpTicket);
                    context.SaveChanges();
                }
            }

            using (var context = new DataContext())
            {
                foreach (var VARIABLE in tickets)
                {
                    Dictionary<string, object> tiketInfo = new Dictionary<string, object>();
                    var actualTicket = context.Tickets.Find(VARIABLE.TicketId);
                    var actualPlace = context.Places.Find(actualTicket.PlacesId);
                    var actualHall = context.Halls.Find(actualPlace.HallId);
                    var actualSession = context.Sessions.Find(actualTicket.SesssionsId);
                    var actualFilm = context.Films.Find(actualSession.FilmId);
                    tiketInfo.Add("ID", actualTicket.TicketId);
                    tiketInfo.Add("Hall", actualHall.Name);
                    tiketInfo.Add("StartTime", actualSession.TimeStart);
                    tiketInfo.Add("Row", actualPlace.Row);
                    tiketInfo.Add("Place", actualPlace.Place1);
                    tiketInfo.Add("FilmName", actualFilm.Name);
                    tiketInfo.Add("Price", actualTicket.Price);
                    yield return tiketInfo;
                }
            }
        }
    }
}