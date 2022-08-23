using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projek1.DTO;

namespace Projek1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> _movieList = new List<Movie>() {
            { new Movie(0, "Dilan 1999", "Romance", "Dilan 1") },
            { new Movie(1, "Dilan 2000", "Romance", "Dilan 2") }
        };

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<MovieDTO>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult GetAll()
        {
            return new OkObjectResult(_movieList);
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(List<MovieDTO>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult GetByTitle([FromQuery] string genre)
        {
            var movie = _movieList.Where(x => x.genre.Contains(genre));
            if (movie.Any())
            {
                return new OkObjectResult(movie);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(MovieDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult Create([FromBody] Movie movie)
        {
            var isExist = _movieList.Where(x => x.id == movie.id).Any();
            if (!isExist)
            {
                _movieList.Add(movie);
                return new OkResult();
            }
            return new BadRequestResult();
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(typeof(MovieDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult Update([FromBody] MovieDTO movie, [FromRoute] int id)
        {
            var result = _movieList.FirstOrDefault(x => x.id == id);
            if (result != null)
            {
                result.title = movie.title;
                result.genre = movie.genre;
                result.sinopsis = movie.sinopsis;
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(MovieDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult Delete([FromRoute] int id)
        {
            var movie = _movieList.Where(x => x.id == id);
            if (movie.Any())
            {
                _movieList.Remove(movie.First());
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }

        }
    }
}
