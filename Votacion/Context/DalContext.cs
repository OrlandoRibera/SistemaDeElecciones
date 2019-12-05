using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SistemaElecciones.Models;

namespace Votacion.Context
{
    public class DalContext : DbContext
    {
        public DalContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Jurado> Jurado { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Partido> Partido { get; set; }
        public DbSet<TipoVoto> TipoVoto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Voto> Voto { get; set; }
    }
}
