using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MindboxTestApp.Core.Entities;

namespace MindboxTestApp.Core
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Circle> Circles { get; set; }

        public DbSet<Triangle> Triangles { get; set; }

        public DbSet<Ellipse> Ellipses { get; set; }
    }
}