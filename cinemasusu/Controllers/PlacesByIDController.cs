using cinemasusu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace cinemasusu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacesByIDController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<object> Get(int ID) //session
        {
            using (var context = new cinemaContext())
            {
                // http://localhost:14050/PlacesByID?ID=8
                Session _session = context.Sessions.Find(ID);
                var Places =
                    from places in context.Set<Place>().Where(place => place.HallId == _session.HallId)
                    join ticket in context.Set<Ticket>().Where(season => season.SesssionsId == ID)
                        on places.PlaceId equals ticket.PlacesId into grouping1
                    from subpet in grouping1.DefaultIfEmpty()
                    select new { places.PlaceId, places.Row, places.Place1, Offset = places.Offset == null ? 0 : places.Offset, placeIsTaken = subpet == null ? 0 : 1 };
                return Places.ToList();
            }
        }
    }
}
