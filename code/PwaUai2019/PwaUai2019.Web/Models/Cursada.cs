﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using PwaUai2019.Web.Helpers;

namespace PwaUai2019.Web.Models
{
    public class Cursada
    {
        public long Id { get; set; }

        public string Materia { get; set; }

        public string Carrera { get; set; }

        [DisplayName("Cantidad de Alumnos")]
        public int CantidadAlumnos { get; set; }

        public Turno Turno { get; set; }

        public Day Dia { get; set; }

        public string Comision { get; set; }

        public long AulaId { get; set; }

        [JsonIgnore]
        public Aula Aula { get; set; }
    }
}
