namespace Database;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        string path = Directory.GetCurrentDirectory();
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder.SetBasePath(path)
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
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Characters");
        context.Database.ExecuteSqlRaw("DELETE TABLE IF EXISTS dbo.Actors");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Characters', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Actors', RESEED, 0)");

        //var mads = new UserDTO{"Mads cornelius"};

        context.Users.AddRange(
            new UserDTO { userID = 1, userName = "Mads", email = "coha@itu.dk" }
        );

        context.SaveChanges();
    }
}