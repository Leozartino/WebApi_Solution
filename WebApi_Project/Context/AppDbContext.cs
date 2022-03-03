﻿using Microsoft.EntityFrameworkCore;
using WebApi_Project.Models;

namespace WebApi_Project.Context
{   //O contexto vai encapsular todas as nossas entidades 
    public class AppDbContext : DbContext
    {   
        // Existe um padrão quando a gente usa o EntityFramework
        // para nomear listas no plural
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuild)
        {
            optionsBuild.UseSqlServer("Password=1525@Xyzt;Persist Security Info=True;User ID=sa;Initial Catalog=first_database;Data Source=DESKTOP-9P4JB41\SQLEXPRESS");
        }
    }
}