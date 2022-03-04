using Microsoft.EntityFrameworkCore;
using WebApi_Project.Models;

namespace WebApi_Project.Context
{   //O contexto vai encapsular todas as nossas entidades 
    public class AppDbContext : DbContext
    {    
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        // Existe um padrão quando a gente usa o EntityFramework
        // para nomear listas no plural
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuild)
        {    
            // Posso mudar o nome do banco e no caso do windows devmeos passar o @ para forçar o interpreter entender a string com backslash
            optionsBuild.UseSqlServer(@"Password=1525@Xyzt;Persist Security Info=True;User ID=sa;Initial Catalog=first_database;Data Source=DESKTOP-9P4JB41\SQLEXPRESS");
        }
    }
}
