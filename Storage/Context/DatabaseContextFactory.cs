namespace SE_training.Context;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        string path = Directory.GetCurrentDirectory();
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder
            .SetBasePath(path)
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("SETraining");

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlServer(connectionString);

        return new DatabaseContext(optionsBuilder.Options);
    }

    public static void Seed(DatabaseContext context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Comments");
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Ratings");
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Tags");
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Materials");
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Users");

        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Materials', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tags', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Ratings', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Comments', RESEED, 0)");

        var mads = new User{Name = "Mads Cornelius", Email = "coha@itu.dk"};
        context.Users.AddRange(
            mads
        );

        context.SaveChanges();
    }
}