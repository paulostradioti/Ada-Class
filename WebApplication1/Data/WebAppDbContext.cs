using Application.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class WebAppDbContext : DbContext
    {
        public DbSet<Turma> Turmas => Set<Turma>();
        public DbSet<Aluno> Alunos => Set<Aluno>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var _dbPath = Path.Combine(localAppData, "WebApp.db");
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }
    }
}
