using Xunit;

namespace Infrastructure.Tests;

public class SearchEngineTests : IDisposable
{
    private readonly IDatabaseContext _context;
    private readonly SearchEngine _searchEngine;
    private bool _disposed;

    public SearchEngineTests()
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
            new Material() { Id = 1, AuthorId = 3, Name = "How to make a dockerfile", Description = "Also docker composed will be explained in better detail", FileType = Link, FilePath = "https://learnit.itu.dk/course/view.php?id=3020927" },
            new Material() { Id = 2, AuthorId = 3, Name = "The best way to run your program", Description = "spoler alert: use Docker ;)", FileType = Pdf, FilePath = "https://github.com/ondfisk/BDSA2021/blob/main/Slides/Lecture08%20-%20ASP.NET%20Core%20Web%20API%20part%20deux.Pdf" },
            new Material() { Id = 3, AuthorId = 4, Name = "Prooving the impossible: 0 / 0 = 42", Description = "You better watch out! You better not cry!", FileType = Mp4, FilePath = "https://www.youtube.com/watch?v=BRRolKTlF6Q" },
            new Material() { Id = 4, AuthorId = 4, Name = "How to make a true italian dessert ;)", Description = "Remember lots of butter!", FileType = Gif, FilePath = "https://www.youtube.com/watch?v=9xwbFww5LkM" });

        context.Tags.AddRange(
            new Tag() { Id = 1, MaterialId = 1, TagName = "Docker" },
            new Tag() { Id = 2, MaterialId = 2, TagName = "Docker" },
            new Tag() { Id = 3, MaterialId = 2, TagName = "BDSA" },
            new Tag() { Id = 4, MaterialId = 4, TagName = "Tiramisu" });

        context.Comments.AddRange(
            new Comment() { Id = 1, MaterialId = 1, UserId = 1, Text = "What is Docker?" },
            new Comment() { Id = 2, MaterialId = 3, UserId = 3, Text = "But I thought it should equal 69?" },
            new Comment() { Id = 3, MaterialId = 4, UserId = 1, Text = "So delicious ;)" },
            new Comment() { Id = 4, MaterialId = 3, UserId = 2, Text = "Can you explain in further detail." });

        context.Ratings.AddRange(
            new Rating() { Id = 1, MaterialId = 4, UserId = 1, Value = 3 },
            new Rating() { Id = 2, MaterialId = 2, UserId = 1, Value = 2 },
            new Rating() { Id = 3, MaterialId = 4, UserId = 2, Value = 4 },
            new Rating() { Id = 4, MaterialId = 4, UserId = 1, Value = 5 });

        context.SaveChanges();
        _context = context;
        _searchEngine = new SearchEngine(new UserRepository(context), new MaterialRepository(context), new TagRepository(context), new CommentRepository(context), new RatingRepository(context));
    }

    [Fact]
    public async void Search_given_searchString_returns_nothing()
    {
        var results = await _searchEngine.SearchAsync("java");

        Assert.Empty(results);
    }

    [Fact]
    public async void Search_given_searchString_returns_all_materials_with_any_relation()
    {
        var results = await _searchEngine.SearchAsync(";)");

        Assert.Collection(results,
            material => Assert.Equal(new MaterialDTO(4, 4, "How to make a true italian dessert ;)", "Remember lots of butter!", Gif, "https://www.youtube.com/watch?v=9xwbFww5LkM"), material),
            material => Assert.Equal(new MaterialDTO(2, 3, "The best way to run your program", "spoler alert: use Docker ;)", Pdf, "https://github.com/ondfisk/BDSA2021/blob/main/Slides/Lecture08%20-%20ASP.NET%20Core%20Web%20API%20part%20deux.Pdf"), material)
        );
    }

    [Fact]
    public async void SearchByName_given_searchString_returns_nothing()
    {
        var results = await _searchEngine.SearchByNameAsync("algoritm");

        Assert.Empty(results);
    }

    [Fact]
    public async void SearchByName_given_materialName_returns_all_materials_with_name()
    {
        var results = await _searchEngine.SearchByNameAsync("OW to make ");

        Assert.Collection(results,
            material => Assert.Equal(new MaterialDTO(1, 3, "How to make a dockerfile", "Also docker composed will be explained in better detail", Link, "https://learnit.itu.dk/course/view.php?id=3020927"), material),
            material => Assert.Equal(new MaterialDTO(4, 4, "How to make a true italian dessert ;)", "Remember lots of butter!", Gif, "https://www.youtube.com/watch?v=9xwbFww5LkM"), material)
        );
    }

    [Fact]
    public async void SearchByDescription_given_searchString_returns_nothing()
    {
        var results = await _searchEngine.SearchByDescriptionAsync("what is the meaning of life?");

        Assert.Empty(results);
    }

    [Fact]
    public async void SearchByDescription_given_description_returns_all_materials_with_description()
    {
        var results = await _searchEngine.SearchByDescriptionAsync("better");

        Assert.Collection(results,
            material => Assert.Equal(new MaterialDTO(1, 3, "How to make a dockerfile", "Also docker composed will be explained in better detail", Link, "https://learnit.itu.dk/course/view.php?id=3020927"), material),
            material => Assert.Equal(new MaterialDTO(3, 4, "Prooving the impossible: 0 / 0 = 42", "You better watch out! You better not cry!", Mp4, "https://www.youtube.com/watch?v=BRRolKTlF6Q"), material)          
        );
    }

    [Fact]
    public async void SearchByTags_given_searchString_returns_nothing()
    {
        var results = await _searchEngine.SearchByTagsAsync("cheesecake");

        Assert.Empty(results);
    }

    [Fact]
    public async void SearchByTags_given_tagName_returns_all_materials_with_tag()
    {
        var results = await _searchEngine.SearchByTagsAsync("docker");

        Assert.Collection(results,
            material => Assert.Equal(new MaterialDTO(1, 3, "How to make a dockerfile", "Also docker composed will be explained in better detail", Link, "https://learnit.itu.dk/course/view.php?id=3020927"), material),
            material => Assert.Equal(new MaterialDTO(2, 3, "The best way to run your program", "spoler alert: use Docker ;)", Pdf, "https://github.com/ondfisk/BDSA2021/blob/main/Slides/Lecture08%20-%20ASP.NET%20Core%20Web%20API%20part%20deux.Pdf"), material)
        );
    }

    [Fact]
    public async void SearchByAuthorAsync_given_searchString_returns_nothing()
    {
        var results = await _searchEngine.SearchByAuthorAsync("Iben Carb Wiener");

        Assert.Empty(results);
    }

    [Fact]
    public async void GetDetailedMaterialById_given_id_not_existing_returns_null()
    {
        var result = await _searchEngine.GetDetailedMaterialByIdAsync(5);

        Assert.Null(result);
    }

     [Fact]
    public async void GetDetailedMaterialById_given_id_returns_material_inclusive_tags_comments_rating()
    {
        var result = await _searchEngine.GetDetailedMaterialByIdAsync(3);

        Assert.Equal(
            new MaterialDTO(3, 4, "Prooving the impossible: 0 / 0 = 42", "You better watch out! You better not cry!", "mp4", "https://www.youtube.com/watch?v=BRRolKTlF6Q"),
            new MaterialDTO(result.Id, result.AuthorId, result.Name, result.Description, result.FileType, result.FilePath));
        Assert.Collection(result.Tags);
        Assert.Collection(result.Comments,
            comment => Assert.Equal(new CommentDTO(2, 3, 3, "But I thought it should equal 69?"), comment),
            comment => Assert.Equal(new CommentDTO(4, 3, 2, "Can you explain in further detail."), comment)
        );
        Assert.Equal(0, result.Rating);
    }

    [Fact]
    public async void SearchByAuthor_given_authorName_returns_all_materials_by_author()
    {
        var results = await _searchEngine.SearchByAuthorAsync("bru");

        Assert.Collection(results,
            material => Assert.Equal(new MaterialDTO(3, 4, "Prooving the impossible: 0 / 0 = 42", "You better watch out! You better not cry!", Mp4, "https://www.youtube.com/watch?v=BRRolKTlF6Q"), material),
            material => Assert.Equal(new MaterialDTO(4, 4, "How to make a true italian dessert ;)", "Remember lots of butter!", Gif, "https://www.youtube.com/watch?v=9xwbFww5LkM"), material)
        );
    }

    [Fact]
    public async void GetRelatedMaterialsByTags_given_materialId_returns_nothing()
    {
        var results = await _searchEngine.GetRelatedMaterialsByTagsAsync(4);

        Assert.Empty(results);
    }

    [Fact]
    public async void GetRelatedMaterialsByTags_given_materialId_returns_other_materials_with_same_tags()
    {
        var results = await _searchEngine.GetRelatedMaterialsByTagsAsync(1);

        Assert.Collection(results,
            material => Assert.Equal(new MaterialDTO(2, 3, "The best way to run your program", "spoler alert: use Docker ;)", Pdf, "https://github.com/ondfisk/BDSA2021/blob/main/Slides/Lecture08%20-%20ASP.NET%20Core%20Web%20API%20part%20deux.Pdf"), material)
        );
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