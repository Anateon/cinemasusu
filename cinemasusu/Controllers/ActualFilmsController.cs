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
    public class ActualFilmsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Object> Get()
        {
            using (var context = new cinemaContext())
            {
                //var selectActualFilms = context.Films.Join(
                //    context.Sessions,
                //    a => a.FilmId,
                //    b => b.FilmId,
                //    (a, b) => new
                //    {
                //        a.FilmId,
                //        a.Name,
                //        a.AgeRating,
                //        a.Price,
                //        a.Duration,
                //        b.TimeStart,
                //        b.HallId
                //    }).Where(a => a.TimeStart >= DateTime.Now);

                //var selectActualFilms = from film in context.Set<Film>()
                //    from session in context.Set<Session>().Where(session => film.FilmId == session.FilmId).Where(session => session.TimeStart >= DateTime.Now).DefaultIfEmpty()
                //                        select new {film};

                //var ActualFilms =
                //    from film in context.Set<Film>()
                //    join session in context.Set<Session>()
                //        on film.FilmId equals session.FilmId into grouping1
                //    from session in grouping1//.Where(session => session.TimeStart >= DateTime.Today)
                //    select new { film.FilmId, film.Name, film.AgeRating, film.Duration, session.TimeStart, session.Hall, session.SessionId};
                // http://localhost:14050/ActualFilms

                var ActualFilms =
                    from film in context.Set<Film>()
                    join session in context.Set<Session>()
                        on film.FilmId equals session.FilmId into grouping1
                    from session in grouping1//.Where(session => session.TimeStart >= DateTime.Now)
                    select new { film.FilmId, film.Name, film.AgeRating, film.Duration };

                var Ganres =
                    from genresAndFilm in context.Set<GenresAndFilm>()
                    join session in context.Set<Session>()
                        on genresAndFilm.FilmsId equals session.FilmId into grouping1
                    from session in grouping1//.Where(session => session.TimeStart >= DateTime.Now)
                    join genre in context.Set<Genre>()
                        on genresAndFilm.GenresId equals genre.GenreId
                    select new { genresAndFilm.FilmsId, genreName = genre.Name };

                var Places =
                    from film in context.Set<Film>()
                    join session in context.Set<Session>()
                        on film.FilmId equals session.FilmId into grouping1
                    from session in grouping1//.Where(session => session.TimeStart >= DateTime.Now)
                    join hall in context.Set<Hall>()
                        on session.HallId equals hall.HallId
                    select new { session.FilmId, session.SessionId, hall.Name, hall.ImaxStatus, hall._3dStatus, hall.HallId, session.TimeStart };

                yield return ActualFilms.ToList().Distinct();
                yield return Ganres.ToList().Distinct();
                yield return Places.ToList().Distinct().OrderBy(d => d.TimeStart);
            }
        }
    }
}
