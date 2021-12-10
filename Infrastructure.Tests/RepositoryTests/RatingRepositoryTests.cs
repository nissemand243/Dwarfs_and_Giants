using Xunit;

namespace Infrastructure.Tests;

public class RatingRepositoryTests : IDisposable
{
    private readonly IDatabaseContext _context;
    private readonly RatingRepository _repo;
    private bool _disposed;

    public RatingRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var context = new DatabaseContext(builder.Options);
        context.Database.EnsureCreated();
        context.Ratings.AddRange(
            new Rating() { Id = 1, MaterialId = 11, UserId = 1, Value = 5 },
            new Rating() { Id = 2, MaterialId = 22, UserId = 1, Value = 1 },
            new Rating() { Id = 3, MaterialId = 22, UserId = 2, Value = 5 });
        context.SaveChanges();

        _context = context;
        _repo = new RatingRepository(context);
    }

    [Fact]
    public async void GetAsync_given_id_not_existing_returns_empty()
    {
        var ratings33 = await _repo.ReadAsync(33);

        Assert.Empty(ratings33);
    }

    [Fact]
    public async void GetAsync_given_id_returns_tag()
    {
        var ratings11 = await _repo.ReadAsync(11);

        Assert.Collection(ratings11,
            rating => Assert.Equal(new RatingDTO(1, 11, 1, 5), rating)
        );
    }

    [Fact]
    public async void GetAsync_returns_all_tags()
    {
        var tags = await _repo.ReadAsync();

        Assert.Collection(tags,
            tag => Assert.Equal(new RatingDTO(1, 11, 1, 5), tag),
            tag => Assert.Equal(new RatingDTO(2, 22, 1, 1), tag),
            tag => Assert.Equal(new RatingDTO(3, 22, 2, 5), tag)
        );
    }

    [Fact]
    public async void PutAsync_given_new_entity_returns_created()
    {
        var result = await _repo.CreateAsync(new CreateRatingDTO(22, 3, 5));

        Assert.Equal(Created, result.status);
        Assert.Equal(new RatingDTO(4, 22, 3, 5), result.rating);
    }

    [Fact]
    public async void PostAsync_given_entity_with_id_not_existing_returns_NotFound()
    {
        var status = await _repo.UpdateAsync(new RatingDTO(4, 22, 3, 5));

        Assert.Equal(NotFound, status);
    }

    [Fact]
    public async void PostAsync_given_entity_returns_Updated()
    {
        var status = await _repo.UpdateAsync(new RatingDTO(1, 11, 1, 1));
        var ratings11 = await _repo.ReadAsync(11);

        Assert.Equal(Updated, status);
        Assert.Collection(ratings11,
            rating => Assert.Equal(new RatingDTO(1, 11, 1, 1), rating)
        );
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