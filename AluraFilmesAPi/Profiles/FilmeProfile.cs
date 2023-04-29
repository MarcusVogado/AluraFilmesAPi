using AluraFilmesAPi.Data.Dtos;
using AluraFilmesAPi.Models;
using AutoMapper;

namespace AluraFilmesAPi.Profiles
{
    public class FilmeProfile:Profile
    {
        public FilmeProfile()
        {
            CreateMap<FilmeDTO,Filme>();
        } 
    }
}
