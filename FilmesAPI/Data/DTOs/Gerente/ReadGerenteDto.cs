﻿using System.Collections.Generic;

namespace FilmesAPI.Data.DTOs.Gerente
{
    public class ReadGerenteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public  object Cinemas { get; set; }
    }
}