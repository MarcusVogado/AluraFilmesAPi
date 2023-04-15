using AluraFilmesAPi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AluraFilmesAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        [HttpPost]
        [Route("AddFilme")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {         
            
                filmes.Add(filme);
                return Ok(filmes);
           
        }
    }
}
