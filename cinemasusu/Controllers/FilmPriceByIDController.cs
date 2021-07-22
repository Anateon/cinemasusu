using cinemasusu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cinemasusu.Models;

namespace cinemasusu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmPriceByIDController : ControllerBase
    {
        // http://localhost:14050/FilmPriceByID?ID=8
        [HttpGet]
        public decimal Get(int ID)
        {
            using (var context = new DataContext())
            {
                var session = context.Sessions.Find(ID);
                var filmPrice = context.Films.Find(session.FilmId).Price;
                var price3d = context.AddedValues.FirstOrDefault(p => p.Name == "3d");
                var priceimax = context.AddedValues.FirstOrDefault(p => p.Name == "imax");
                var hall = context.Halls.Find(session.HallId);
                var fullprice = filmPrice;
                if (hall.ImaxStatus)
                {
                    fullprice += priceimax.Price;
                }

                if (hall._3dStatus)
                {
                    fullprice += price3d.Price;
                }
                return fullprice;
            }
        }
    }
}
