using Microsoft.EntityFrameworkCore;
using SistemaGestaoCantinasIgrejas.Models.Consulta;

namespace SistemaGestaoCantinasIgrejas.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Igreja> igreja { get; set; }
        public DbSet<Evento> evento { get; set; }
        public DbSet<Participante> participante { get; set; }
        public DbSet<Produto> produto { get; set; }
        public DbSet<Venda> venda { get; set; }
        public DbSet<SistemaGestaoCantinasIgrejas.Models.Consulta.PivotMes> PivotMes { get; set; }
    }
}
