using AluraFilmesAPi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AluraFilmesAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        [HttpPost]
        [Route("AddFilme")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            var idcount = filmes.Count + 1;
            filme.Id = idcount;
            filmes.Add(filme);
            return CreatedAtAction(nameof(GetFilmeId),new {id= filme.Id},filme);
        }
        [HttpGet]
        [Route("GetFilmes")]
        public IEnumerable<Filme> GetFilmesAsync([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return filmes.Skip(skip).Take(take);
        }
        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetFilmeId(int Id)
        {

            Filme? filme = filmes.Find(i => i.Id == Id);
            if (filme == null) { return NotFound("Nenhum Filme Encontrado"); }
            return Ok(filme);
        }

    }
}
