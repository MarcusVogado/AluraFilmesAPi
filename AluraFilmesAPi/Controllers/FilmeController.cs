using AluraFilmesAPi.Data;
using AluraFilmesAPi.Data.Dtos;
using AluraFilmesAPi.Models;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AluraFilmesAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FilmeController : ControllerBase
    {
        private FilmeContext _data;
        private IMapper _mapper;
        public FilmeController(FilmeContext data,IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDTO">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [Route("AddFilme")]
        public IActionResult AddFilme([FromBody] FilmeDTO filmeDTO)
        {
            
            Filme filme =_mapper.Map<Filme>(filmeDTO);
            _data.Filmes.Add(filme);
            _data.SaveChanges();
            return CreatedAtAction(nameof(GetFilmeId), new { id = filme.Id }, filme);
        }/// <summary>
         /// Pesquisa todos os filmes criados
         /// </summary>
         /// <param name="skip">Objeto necessário para pesquisa</param>
         /// <returns>IEnumerable</returns>
         /// <response code="200">Se a pesquisa for um sucesso</response>
        [HttpGet]
        [Route("GetFilmes")]
        public IEnumerable<Filme> GetFilmesAsync([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _data.Filmes.Skip(skip).Take(take);
        }
        [HttpGet("{Id}")]        
        public async Task<IActionResult> GetFilmeId(int Id)
        {

            var filme = await _data.Filmes.FindAsync(Id);
            if (filme == null) { return NotFound("Nenhum Filme Encontrado"); }
            return Ok(filme);
        }
        [HttpPut("{Id}")]        
        public async Task<IActionResult> UpdateFilme(int Id,[FromBody] FilmeDTO filmeDTO)
        {

            var filme = await _data.Filmes.FindAsync(Id);
            if (filme == null)
            {
                return NotFound("Filme não Encontrado");
            }         
            _mapper.Map(filmeDTO,filme);
            _data.Filmes.Update(filme);
            await _data.SaveChangesAsync();
            return NoContent();

        }
        [HttpPatch("{Id}")]

        public async Task<IActionResult> UpdatePathFilme(int Id, JsonPatchDocument<FilmeDTO> patch)
        {

            var filme = await _data.Filmes.FindAsync(Id);
            if (filme == null)
            {
                return NotFound("Filme não Encontrado");
            }
            var filmeParaAtualizar = _mapper.Map<FilmeDTO>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState);
            if(!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(filmeParaAtualizar, filme);
            _data.Filmes.Update(filme);
            await _data.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{Id}")]       
        public async Task<IActionResult> DeleteFilme(int Id)
        {
            var filme = await _data.Filmes.FindAsync(Id);
            if (filme == null)
            {
                return NotFound("Filme não Encontrado ou já excluido");
            }
            _data.Filmes.Remove(filme);
            await _data.SaveChangesAsync();
            return NoContent();
        } 
    }
}
