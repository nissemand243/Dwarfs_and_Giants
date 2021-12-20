using Xunit;

namespace Infrastructure.Tests;

public class CommentRepositoryTests : IDisposable
{
    private readonly IDatabaseContext _context;
    private readonly CommentRepository _repo;
    private bool _disposed;

    public CommentRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var context = new DatabaseContext(builder.Options);
        context.Database.EnsureCreated();
        context.Comments.AddRange(
            new Comment() { Id = 1, UserId = 1, MaterialId = 11, Text = "Nice work guys!" },
            new Comment() { Id = 2, UserId = 1, MaterialId = 22, Text = "What is Docker?" },
            new Comment() { Id = 3, UserId = 1, MaterialId = 22, Text = "Can you explain in further detail." });
        context.SaveChanges();

        _context = context;
        _repo = new CommentRepository(context);
    }

    [Fact]
    public async void ReadAsync_given_id_not_existing_returns_empty()
    {
        var comments33 = await _repo.ReadAsync(33);

        Assert.Empty(comments33);
    }

    [Fact]
    public async void ReadAsync_given_id_returns_comment()
    {
        var comments11 = await _repo.ReadAsync(11);

        Assert.Collection(comments11,
            comment => Assert.Equal(new CommentDTO(1, 11, 1, "Nice work guys!"), comment)
        );
    }

    [Fact]
    public async void ReadAllAsync_returns_all_comments()
    {
        var comments = await _repo.ReadAllAsync();

        Assert.Collection(comments,
            comment => Assert.Equal(new CommentDTO(1, 11, 1, "Nice work guys!"), comment),
            comment => Assert.Equal(new CommentDTO(2, 22, 1, "What is Docker?"), comment),
            comment => Assert.Equal(new CommentDTO(3, 22, 1, "Can you explain in further detail."), comment)
        );
    }

    [Fact]
    public async void CreateAsync_given_new_entity_returns_created()
    {
        var result = await _repo.CreateAsync(new CreateCommentDTO(33, 2, "Awesome"));

        Assert.Equal(Created, result.status);
        Assert.Equal(new CommentDTO(4, 33, 2, "Awesome"), result.comment);
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
        var comments11 = await _repo.ReadAsync(11);

        Assert.Equal(Deleted, status);
        Assert.Empty(comments11);
    }

    [Fact]
    public async void DeleteAllAsync_given_existing_materialid_return_status_Deleted()
    {
        // Arrange
        var expected = Deleted;
        
        // Act
        var actual = await _repo.DeleteAllAsync(22);
    
        // Asssert
        Assert.Equal(expected, actual);
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