using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using System.IO;
using WebApi_Project.Models;

namespace WebApi_Project.Context
{   //O contexto vai encapsular todas as nossas entidades 
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    // Existe um padrão quando a gente usa o EntityFramework
    // para nomear listas no plural
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    // public DbSet<User> Users { get; set; }
  }
}
