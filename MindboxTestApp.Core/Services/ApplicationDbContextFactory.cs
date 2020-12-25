using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MindboxTestApp.Core.Services
{
    [ExcludeFromCodeCoverage]
    public sealed class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public ApplicationDbContextFactory(DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext(_dbContextOptions);
        }
    }
}