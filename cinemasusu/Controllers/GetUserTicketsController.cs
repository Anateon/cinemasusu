using cinemasusu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using cinemasusu.Authorization;

namespace cinemasusu.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GetUserTicketsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<object> Get()
        {
            List<Ticket> tickets = new List<Ticket>();
            using (var context = new DataContext())
            {
                tickets =
                    (from ticket in context.Set<Ticket>()
                        .Where(Ticket => Ticket.UserId == ((User) this.HttpContext.Items["User"]).Id)
                    select new Ticket()
                    {
                        PlacesId = ticket.PlacesId,
                        Price = ticket.Price,
                        SesssionsId = ticket.SesssionsId,
                        TicketId = ticket.TicketId,
                        UserId = ticket.UserId
                    }).ToList();
                tickets.Sort((ticket, ticket1) =>
                {
                    if (ticket.TicketId < ticket1.TicketId)
                    {
                        return -1;
                    }
                    if (ticket.TicketId > ticket1.TicketId)
                    {
                        return 1;
                    }
                    return 0;
                });
                foreach (var VARIABLE in tickets)
                {
                    Dictionary<string, object> tiketInfo = new Dictionary<string, object>();
                    var actualPlace = context.Places.Find(VARIABLE.PlacesId);
                    var actualHall = context.Halls.Find(actualPlace.HallId);
                    var actualSession = context.Sessions.Find(VARIABLE.SesssionsId);
                    var actualFilm = context.Films.Find(actualSession.FilmId);
                    tiketInfo.Add("ID", VARIABLE.TicketId);
                    tiketInfo.Add("Hall", actualHall.Name);
                    tiketInfo.Add("StartTime", actualSession.TimeStart);
                    tiketInfo.Add("Row", actualPlace.Row);
                    tiketInfo.Add("Place", actualPlace.Place1);
                    tiketInfo.Add("FilmName", actualFilm.Name);
                    tiketInfo.Add("Price", VARIABLE.Price);
                    yield return tiketInfo;
                }

            }
        }
    }
}
