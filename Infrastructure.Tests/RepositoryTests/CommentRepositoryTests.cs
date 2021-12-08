using Xunit;

namespace Infrastructure.Tests;

public class CommentRepositoryTests : IDisposable
{
    private readonly IDatabaseContext context;
    private readonly CommentRepository repo;
    private bool disposed;

    public CommentRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var _context = new DatabaseContext(builder.Options);
        _context.Database.EnsureCreated();
        _context.Comments.AddRange(
            new Comment("Nice work guys!") { Id = 1, UserId = 1, MaterialId = 11 },
            new Comment("What is Docker?") { Id = 2, UserId = 1, MaterialId = 22},
            new Comment("Can you explain in further detail.") { Id = 3, UserId = 1, MaterialId = 22});
        _context.SaveChanges();

        context = _context;
        repo = new CommentRepository(_context);
    }

    [Fact]
    public async void GetAsync_given_id_not_existing_returns_empty()
    {
        var comments33 = await repo.GetAsync(33);

        Assert.Empty(comments33);
    }

    [Fact]
    public async void GetAsync_given_id_returns_comment()
    {
        var comments11 = await repo.GetAsync(11);

        Assert.Collection(comments11,
            comment => Assert.Equal(new CommentDTO(1, 11, 1, "Nice work guys!"), comment)
        );
    }

    [Fact]
    public async void GetAsync_returns_all_comments()
    {
        var comments = await repo.GetAsync();

        Assert.Collection(comments,
            comment => Assert.Equal(new CommentDTO(1, 11, 1, "Nice work guys!"), comment),
            comment => Assert.Equal(new CommentDTO(2, 22, 1, "What is Docker?"), comment),
            comment => Assert.Equal(new CommentDTO(3, 22, 1, "Can you explain in further detail."), comment)
        );
    }

    [Fact]
    public async void PutAsync_given_new_entity_returns_created()
    {
        var result = await repo.PutAsync(new CreateCommentDTO(33, 2, "Awesome"));

        Assert.Equal(Created, result.status);
        Assert.Equal(new CommentDTO(4, 33, 2, "Awesome"), result.comment);
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
        var comments11 = await repo.GetAsync(11);

        Assert.Equal(Deleted, status);
        Assert.Empty(comments11);
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