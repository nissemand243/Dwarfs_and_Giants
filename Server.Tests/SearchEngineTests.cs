using Xunit;

namespace Infrastructure.Tests;

public class SearchEngineTests : IDisposable
{
    private readonly IDatabaseContext _context;
    private readonly SearchEngine _searchEngine;
    private bool _disposed;

    /*public SearchEngineTests()
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
            new Teacher() { Id = 3, Name = "OndFisk", Email = "evilFish@microsoft.com" },
            new Teacher() { Id = 4, Name = "Bruni", Email = "IHeartMath@foc.dm" });

        context.Materials.AddRange(
            new Material() { Id = 1, AuthorId = 3, Name = "How to make a dockerfile", Description = "Also docker composed will be explained", FileType = link, FilePath = "https://learnit.itu.dk/course/view.php?id=3020927" },
            new Material() { Id = 2, AuthorId = 3, Name = "The best way to run your program", Description = "spoler alert: use Docker ;)", FileType = pdf, FilePath = "https://github.com/ondfisk/BDSA2021/blob/main/Slides/Lecture08%20-%20ASP.NET%20Core%20Web%20API%20part%20deux.pdf" },
            new Material() { Id = 3, AuthorId = 4, Name = "Prooving the impossible: 0 / 0 = 42", Description = "You better watch out! You better not cry!", FileType = mp4, FilePath = "https://www.youtube.com/watch?v=BRRolKTlF6Q" },
            new Material() { Id = 4, AuthorId = 4, Name = "How to make a true italian dessert ;)", Description = "Remember lots of butter!", FileType = gif, FilePath = "https://www.youtube.com/watch?v=9xwbFww5LkM" });

        context.Tags.AddRange(
            new Tag() { Id = 1, MaterialId = 1, TagName = "Docker" },
            new Tag() { Id = 2, MaterialId = 2, TagName = "Docker" },
            new Tag() { Id = 3, MaterialId = 2, TagName = "BDSA" },
            new Tag() { Id = 4, MaterialId = 4, TagName = "Tiramisu" });

        context.Comments.AddRange(
            new Comment() { Id = 1, UserId = 1, MaterialId = 1, Text = "What is Docker?" },
            new Comment() { Id = 2, UserId = 3, MaterialId = 3, Text = "But I thought it should equal 69?" },
            new Comment() { Id = 3, UserId = 1, MaterialId = 4, Text = "So delicious ;)" },
            new Comment() { Id = 2, UserId = 2, MaterialId = 3, Text = "Can you explain in further detail." });

        context.Ratings.AddRange(
            new Rating() { Id = 1, MaterialId = 2, UserId = 1, Value = 3 },
            new Rating() { Id = 2, MaterialId = 4, UserId = 1, Value = 2 },
            new Rating() { Id = 3, MaterialId = 4, UserId = 2, Value = 4 },
            new Rating() { Id = 4, MaterialId = 4, UserId = 1, Value = 5 });

        context.SaveChanges();
        _context = context;
        _searchEngine = new SearchEngine(new UserRepository(context), new MaterialRepository(context), new TagRepository(context), new CommentRepository(context), new RatingRepository(context));
    }

    [Fact]
    public void Test()
    {

    }*/

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