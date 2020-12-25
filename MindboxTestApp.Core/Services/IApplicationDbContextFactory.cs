namespace MindboxTestApp.Core.Services
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}