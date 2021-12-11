using Xunit;

namespace Infrastructure.Tests;

public class TagRepositoryTests : IDisposable
{
    private readonly IDatabaseContext _context;
    private readonly TagRepository _repo;
    private bool _disposed;

    public TagRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var context = new DatabaseContext(builder.Options);
        context.Database.EnsureCreated();
        context.Tags.AddRange(
            new Tag() { Id = 1, MaterialId = 11, TagName = "Docker" },
            new Tag() { Id = 2, MaterialId = 22, TagName = "Docker" },
            new Tag() { Id = 3, MaterialId = 22, TagName = "BDSA" });
        context.SaveChanges();

        _context = context;
        _repo = new TagRepository(context);
    }

    [Fact]
    public async void ReadAsync_given_id_not_existing_returns_empty()
    {
        var tags33 = await _repo.ReadAsync(33);

        Assert.Empty(tags33);
    }

    [Fact]
    public async void ReadAsync_given_id_returns_tag()
    {
        var tags11 = await _repo.ReadAsync(11);

        Assert.Collection(tags11,
            tag => Assert.Equal(new TagDTO(1, 11, "Docker"), tag)
        );
    }

    [Fact]
    public async void ReadAsync_returns_all_tags()
    {
        var tags = await _repo.ReadAsync();

        Assert.Collection(tags,
            tag => Assert.Equal(new TagDTO(1, 11, "Docker"), tag),
            tag => Assert.Equal(new TagDTO(2, 22, "Docker"), tag),
            tag => Assert.Equal(new TagDTO(3, 22, "BDSA"), tag)
        );
    }

    [Fact]
    public async void CreateAsync_given_new_entity_returns_created()
    {
        var result = await _repo.CreateAsync(new CreateTagDTO(33, "UML"));

        Assert.Equal(Created, result.status);
        Assert.Equal(new TagDTO(4, 33, "UML"), result.tag);
    }

    [Fact]
    public async void DeleteAsync_given_id_not_existing_returns_NotFound()
    {
        var status = await _repo.DeleteAsync(4);

        Assert.Equal(NotFound, status);
    }

    [Fact]
    public async void DeleteAsync_given_id_returns_Deleted()
    {
        var status = await _repo.DeleteAsync(1);
        var tags11 = await _repo.ReadAsync(11);

        Assert.Equal(Deleted, status);
        Assert.Empty(tags11);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}