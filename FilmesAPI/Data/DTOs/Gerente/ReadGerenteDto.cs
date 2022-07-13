using System.Collections.Generic;

namespace FilmesAPI.Data.DTOs.Gerente
{
    public class ReadGerenteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public  List<Models.Cinema> Cinemas { get; set; }
    }
}