// See https://aka.ms/new-console-template for more information
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var configuration = LoadConfiguration();
        var connectionString = configuration.GetConnectionString("SE_training");

        Console.WriteLine("cs:" + connectionString);
        connectionString = "Server=localhost;Database=SE_training;User Id=sa;Password=7872816a-0763-40c0-beab-d7ee994018bf";

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connectionString);
        using var context = new DatabaseContext(optionsBuilder.Options);
        DatabaseContextFactory.Seed(context);

        foreach (var user in context.Users.Include(u => u.userName).AsNoTracking())
        {
            Console.WriteLine(user);
        }
    }

    static IConfiguration LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Context/appsettings.json")
            .AddUserSecrets<Program>();

        return builder.Build();
    }
}
