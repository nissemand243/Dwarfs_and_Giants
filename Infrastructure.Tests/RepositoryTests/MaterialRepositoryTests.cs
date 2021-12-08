using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests;

public class MaterialRepositoryTests : IDisposable
{
    private readonly IDatabaseContext context;
    private readonly MaterialRepository repo;
    private bool disposed;

    public MaterialRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var _context = new DatabaseContext(builder.Options);
        _context.Database.EnsureCreated();
        _context.Materials.AddRange(
            new Material(){Id = 1, AuthorId = 1, Name = "Docker is (not) fun", Description = "blabladockerblabla" },
            new Material(){Id = 2, AuthorId = 1, Name = "C# global using functionality", Description = "Global using is an easy way to import packages globally" },
            new Material(){Id = 3, AuthorId = 2, Name = "SwEng theory is tough", Description = "Theory for Software engineering is difficult, but very useful when developing larger systems." });
        _context.SaveChanges();

        context = _context;
        repo = new MaterialRepository(_context);
    }

    [Fact]
    public async void CreateAsync_returns_matching_material()
    {
        
    }
    
    [Fact]
    public async void ReadAsync_given_id_not_existing_returns_empty()
    {
        var material42 = await repo.ReadAsync(42);
        
        Assert.Null(material42);
    }

    [Fact]
    public async void ReadAsync_given_id_returns_material()
    {
        
    }

    [Fact]
    public async void ReadAsync_returns_all_material()
    {
    }

    [Fact]
    public async void PutAsync_given_new_entity_returns_created()
    {
        
    }

    [Fact]
    public async void DeleteAsync_given_id_not_existing_returns_NotFound()
    {
        
    }

    [Fact]
    public async void DeleteAsync_given_id_returns_Deleted()
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