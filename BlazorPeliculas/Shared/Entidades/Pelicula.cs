﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPeliculas.Shared.Entidades
{
    //null! /=> Perdonar el nulo, para indicar que no haya un problema de que Titulo este sin inicializar
    // ? => Para que sea nulleable
    public class Pelicula
   {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        public string? Resumen { get; set; }
        public bool EnCartelera { get; set; }
        public string? Trailer { get; set; }
        public DateTime? Lanzamiento { get; set; }
        public List<GeneroPelicula> GenerosPelicula { get; set; } = new List<GeneroPelicula>();
        public List<PeliculaActor> PeliculasActor { get; set; } = new List<PeliculaActor>();
        public List<VotoPelicula> VotosPeliculas { get; set; } = new List<VotoPelicula>();
        public string? Poster { get; set; }
        public byte[]? Poster2 { get; set; } 

        public string TituloCortado
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Titulo))
                {
                    return null;
                }
                if (Titulo.Length > 60)
                {
                    return Titulo.Substring(0, 60) + "...";
                }
                else
                {
                    return Titulo;
                }
            }
        }
   }
}
