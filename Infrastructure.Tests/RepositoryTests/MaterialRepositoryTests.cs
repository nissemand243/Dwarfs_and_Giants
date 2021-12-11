using System.Collections.Generic;
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
            new Material(){Id = 1, AuthorId = 1, Name = "Docker is (not) fun", Description = "blabladockerblabla",FileType = FileType.Pdf, FilePath = "MaterialsData/test.pdf" },
            new Material(){Id = 2, AuthorId = 1, Name = "C# global using functionality", Description = "Global using is an easy way to import packages globally",FileType = FileType.Pdf, FilePath = "MaterialsData/test.pdf" },
            new Material(){Id = 3, AuthorId = 2, Name = "SwEng theory is tough", Description = "Theory for Software engineering is difficult, but very useful when developing larger systems.", FileType = FileType.Pdf, FilePath = "MaterialsData/test.pdf" });
        _context.SaveChanges();

        context = _context;
        repo = new MaterialRepository(_context);
    }

     [Fact]
    public async void CreateAsync_returns_matching_material()
    {
        throw new NotImplementedException();
    } 
    
    [Fact]
    public async void CreateAsync_given_new_entity_returns_created()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public async void ReadAsync_given_id_not_existing_returns_empty()
    {
        var material42 = await repo.ReadAsync(42);
        
        Assert.Null(material42);
    }

    [Fact]
    public async void ReadAsync_given_id_returns_materialDTO()
    {
        var material2 = await repo.ReadAsync(2);
        
        var expMaterial = new MaterialDTO(2, 1, "C# global using functionality", "Global using is an easy way to import packages globally", "Pdf","MaterialsData/test.pdf" );
        Assert.Equal(expMaterial, material2);
    }

    [Fact]
    public async void ReadAllAsync_returns_all_material()
    {
        var expMaterialList = new List<MaterialDTO>{new (1, 1, "Docker is (not) fun", "blabladockerblabla", "Pdf", "MaterialsData/test.pdf"),
        new (2, 1, "C# global using functionality",
            "Global using is an easy way to import packages globally", "Pdf", "MaterialsData/test.pdf"),
        new (3, 2, "SwEng theory is tough",
            "Theory for Software engineering is difficult, but very useful when developing larger systems.", "Pdf",
            "MaterialsData/test.pdf")
    };
        var actual = await repo.ReadAllAsync();

    Assert.Equal(expMaterialList, actual);
    }

    [Fact]
    public async void DeleteAsync_given_id_not_existing_returns_NotFound()
    {
        var actual = await repo.DeleteAsync(42);
        
        Assert.Equal(Status.NotFound, actual);
    }

    [Fact]
    public async void DeleteAsync_given_id_returns_Deleted()
    {
        var actual = await repo.DeleteAsync(1);
        
        Assert.Equal(Status.Deleted, actual);
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