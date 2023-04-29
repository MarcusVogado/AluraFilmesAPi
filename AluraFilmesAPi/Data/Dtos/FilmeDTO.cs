using System.ComponentModel.DataAnnotations;

namespace AluraFilmesAPi.Data.Dtos
{
    public class FilmeDTO
    {
       
        [Required(ErrorMessage = "O título do filme é obrigatório")]
        [StringLength(60, ErrorMessage = "O título do filme deve ter no máximo 100 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [StringLength(30, ErrorMessage = "O gênero deve ter no máximo 20 caracteres")]
        public string Genero { get; set; }

        [Required]
        [Range(1, 360, ErrorMessage = "Duração deve ter entre 70 e 600 minutos")]
        public int Duracao { get; set; }

        [Required]
        [StringLength(50)]
        public string Diretor { get; set; }
    }
}
