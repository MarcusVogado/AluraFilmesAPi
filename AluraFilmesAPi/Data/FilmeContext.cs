using AluraFilmesAPi.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraFilmesAPi.Data
{
    public class FilmeContext:DbContext
    {
        public FilmeContext(DbContextOptions <FilmeContext>op) : base(op)
        {
            
        }
        public DbSet<Filme>Filmes { get; set; }
    }
}
