using Microsoft.EntityFrameworkCore;

namespace AulaEntityFramework.Models
{
    // A classe de contexto de banco de dados ou DbContext é responsável por mapear as classes que serão atreladas as tabelas do banco de dados.
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        // Nesta sessão, especificamos as classes do model que serão espelhadas em tabelas do banco de dados.

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
