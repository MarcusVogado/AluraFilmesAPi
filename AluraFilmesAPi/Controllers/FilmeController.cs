using AluraFilmesAPi.Data;
using AluraFilmesAPi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AluraFilmesAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FilmeController : ControllerBase
    {
        private FilmeContext _data;
        public FilmeController(FilmeContext data)
        {
            _data = data;
        }


        [HttpPost]
        [Route("AddFilme")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {

            _data.Filmes.Add(filme);
            _data.SaveChanges();
            return CreatedAtAction(nameof(GetFilmeId), new { id = filme.Id }, filme);
        }
        [HttpGet]
        [Route("GetFilmes")]
        public IEnumerable<Filme> GetFilmesAsync([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _data.Filmes.Skip(skip).Take(take);
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetFilmeId(int Id)
        {

            var filme = await _data.Filmes.FindAsync(Id);
            if (filme == null) { return NotFound("Nenhum Filme Encontrado"); }
            return Ok(filme);
        }

        [HttpPut]
        [Route("AttFilme")]
        public async Task<IActionResult> AttFilme([FromBody] Filme filme)
        {
            var filmeExist = await _data.Filmes.FindAsync(filme.Id);
            if (filmeExist == null)
            {
                return NotFound("Filme não Encontrado");
            }                      
            _data.Filmes.Update(filme);
            await _data.SaveChangesAsync();
            return Ok("FilmeAtualizado");

        }


    }
}
