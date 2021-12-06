using Xunit;

namespace Infrastructure.Tests;

public class UserRepositoryTests : IDisposable
{
    private readonly DatabaseContext context;
    private readonly UserRepository repo;
    private bool disposed;


    public UserRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var _context = new DatabaseContext(builder.Options);
        _context.Database.EnsureCreated();
        _context.Users.AddRange(new Student() { Id = 1, Name = "Mads Cornelius", Email = "maco@itu.dk" }, new Teacher() { Id = 2, Name = "OndFisk", Email = "evilFish@microsoft.com" });
        _context.SaveChanges();

        context = _context;
        repo = new UserRepository(_context);
    }

    [Fact]
    public void Test1()
    {

    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}