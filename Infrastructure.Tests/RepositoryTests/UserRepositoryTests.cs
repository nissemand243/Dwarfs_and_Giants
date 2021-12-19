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
            new Student() { Id = 1, Name = "Mads Cornelius", Email = "maco@itu.dk" },
            new Student() { Id = 2, Name = "Iben Carb Wiener", Email = "icwiener@gmail.com" },
            new Teacher() { Id = 3, Name = "OndFisk", Email = "evilFish@microsoft.com" });
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
    public async void ReadAsync_given_id_returns_user()
    {
        var user1 = await _repo.ReadAsync(1);

        Assert.Equal(new UserDTO(1, "Mads Cornelius", "maco@itu.dk", "Student"), user1);
    }

    [Fact]
    public async void ReadAsync_given_name_not_existing_returns_null()
    {
        var user4 = await _repo.ReadAsync("Paolo Tell");

        Assert.Null(user4);
    }

    [Fact]
    public async void ReadAsync_given_name_returns_user()
    {
        var user1 = await _repo.ReadAsync("Iben Carb Wiener");

        Assert.Equal(new UserDTO(2, "Iben Carb Wiener", "icwiener@gmail.com", "Student"), user1);
    }

    [Fact]
    public async void ReadAllAsync_returns_all_users()
    {
        var users = await _repo.ReadAllAsync();

        Assert.Collection(users,
            user => Assert.Equal(new UserDTO(1, "Mads Cornelius", "maco@itu.dk", "Student"), user),
            user => Assert.Equal(new UserDTO(2, "Iben Carb Wiener", "icwiener@gmail.com", "Student"), user),
            user => Assert.Equal(new UserDTO(3, "OndFisk", "evilFish@microsoft.com", "Teacher"), user)
        );
    }

    [Fact]
    public async void CreateAsync_given_new_entity_returns_created()
    {
        var result = await _repo.CreateAsync(new CreateUserDTO("Paolo Tell", "pote@itu.dk", "Teacher"));

        Assert.Equal(Created, result.status);
        Assert.Equal(new UserDTO(4, "Paolo Tell", "pote@itu.dk", "Teacher"), result.user);
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
        var user1 = await _repo.ReadAsync(1);

        Assert.Equal(Deleted, status);
        Assert.Null(user1);
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