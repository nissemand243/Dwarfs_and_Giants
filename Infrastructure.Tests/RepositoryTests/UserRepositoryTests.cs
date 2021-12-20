using Xunit;

namespace Infrastructure.Tests;

public class UserRepositoryTests : IDisposable
{
    private readonly IDatabaseContext _context;
    private readonly UserRepository _repo;
    private bool _disposed;

    public UserRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(connection);
        var context = new DatabaseContext(builder.Options);
        context.Database.EnsureCreated();
        context.Users.AddRange(
            new User() { Id = 1, Name = "Mads Cornelius", Email = "maco@itu.dk" },
            new User() { Id = 2, Name = "Iben Carb Wiener", Email = "icwiener@gmail.com" },
            new User() { Id = 3, Name = "OndFisk", Email = "evilFish@microsoft.com" });
        context.SaveChanges();

        _context = context;
        _repo = new UserRepository(context);
    }

    [Fact]
    public async void ReadAsync_given_id_not_existing_returns_null()
    {
        var user4 = await _repo.ReadAsync(4);

        Assert.Null(user4);
    }

    [Fact]
    public async void ReadAsync_given_existing_id_returns_userDTO()
    {
        var actual = await _repo.ReadAsync(3);

        Assert.Equal(new UserDTO(3, "OndFisk", "evilFish@microsoft.com"), actual);
    }

    [Fact]
    public async void ReadAsync_given_email_not_existing_returns_null()
    {
        var noUser = await _repo.ReadAsync("goodShark@gmail.dk");

        Assert.Null(noUser);
    }

    [Fact]
    public async void ReadAsync_given_email_returns_user()
    {
        var user1 = await _repo.ReadAsync("maco@itu.dk");

        Assert.Equal(new UserDTO(1, "Mads Cornelius", "maco@itu.dk"), user1);
    }

    [Fact]
    public async void ReadAllAsync_returns_all_users()
    {
        var users = await _repo.ReadAllAsync();

        Assert.Collection(users,
            user => Assert.Equal(new UserDTO(1, "Mads Cornelius", "maco@itu.dk"), user),
            user => Assert.Equal(new UserDTO(2, "Iben Carb Wiener", "icwiener@gmail.com"), user),
            user => Assert.Equal(new UserDTO(3, "OndFisk", "evilFish@microsoft.com"), user)
        );
    }

    [Fact]
    public async void CreateAsync_given_entity_with_email_existing_returns_conflict()
    {
        var result = await _repo.CreateAsync(new CreateUserDTO("The Copy Cat", "evilFish@microsoft.com"));

        Assert.Equal(Conflict, result.status);
        Assert.Equal(new UserDTO(3, "OndFisk", "evilFish@microsoft.com"), result.user);
    }

    [Fact]
    public async void CreateAsync_given_new_entity_returns_created()
    {
        var result = await _repo.CreateAsync(new CreateUserDTO("Paolo Tell", "pote@itu.dk"));

        Assert.Equal(Created, result.status);
        Assert.Equal(new UserDTO(4, "Paolo Tell", "pote@itu.dk"), result.user);
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