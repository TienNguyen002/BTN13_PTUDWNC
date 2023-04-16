using CookingWeb.Core.Entities;
using CookingWeb.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Data.Contexts
{
    public class WebDbContext :  DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Student> Students { get; set; }

        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CookingWebApp;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseMap).Assembly);
        }
    }
}
