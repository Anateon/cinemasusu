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
    public class AddedValueController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<AddedValue> Get()
        {
            using (var context = new cinemaContext())
            {
                return context.AddedValues.ToList();
            }
        }
    }
}
