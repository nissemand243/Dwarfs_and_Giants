using Xunit;

namespace Infrastructure.Tests;

public class TagRepositoryTests : IDisposable
{
    private readonly IDatabaseContext context;
    private readonly TagRepository repo;
    private bool disposed;

    public TagRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var _context = new DatabaseContext(builder.Options);
        _context.Database.EnsureCreated();
        _context.Tags.AddRange(
            new Tag() { Id = 1, MaterialId = 11, TagName = "Docker" },
            new Tag() { Id = 2, MaterialId = 22, TagName = "Docker" },
            new Tag() { Id = 3, MaterialId = 22, TagName = "BDSA" });
        _context.SaveChanges();

        context = _context;
        repo = new TagRepository(_context);
    }

    [Fact]
    public async void GetAsync_given_id_not_existing_returns_empty()
    {
        var tags33 = await repo.GetAsync(33);

        Assert.Empty(tags33);
    }

    [Fact]
    public async void GetAsync_given_id_returns_tag()
    {
        var tags11 = await repo.GetAsync(11);

        Assert.Collection(tags11,
            tag => Assert.Equal(new TagDTO(1, 11, "Docker"), tag)
        );
    }

    [Fact]
    public async void GetAsync_returns_all_tags()
    {
        var tags = await repo.GetAsync();

        Assert.Collection(tags,
            tag => Assert.Equal(new TagDTO(1, 11, "Docker"), tag),
            tag => Assert.Equal(new TagDTO(2, 22, "Docker"), tag),
            tag => Assert.Equal(new TagDTO(3, 22, "BDSA"), tag)
        );
    }

    [Fact]
    public async void PutAsync_given_new_entity_returns_created()
    {
        var result = await repo.PutAsync(new CreateTagDTO(33, "UML"));

        Assert.Equal(Created, result.status);
        Assert.Equal(new TagDTO(4, 33, "UML"), result.tag);
    }

    [Fact]
    public async void DeleteAsync_given_id_not_existing_returns_NotFound()
    {
        var status = await repo.DeleteAsync(4);

        Assert.Equal(NotFound, status);
    }

    [Fact]
    public async void DeleteAsync_given_id_returns_Deleted()
    {
        var status = await repo.DeleteAsync(1);
        var tags11 = await repo.GetAsync(11);

        Assert.Equal(Deleted, status);
        Assert.Empty(tags11);
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