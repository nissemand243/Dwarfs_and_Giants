namespace SE_training.Infrastructure;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Comment> Comments => Set<Comment>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        //modelBuilder.Entity<Teacher>();
        //modelBuilder.Entity<Student>();

        /*modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();*/

        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Material>().ToTable("Material");

        modelBuilder.Entity<Tag>().ToTable("Tag");
        modelBuilder.Entity<Rating>().ToTable("Rating");
        /*modelBuilder.Entity<Tag>()
                    .HasIndex(t => t.MaterialId)
                    .HasIndex(t => t.TagName)
                    .IsUnique();

        modelBuilder.Entity<Rating>()
                    .HasIndex(r => r.MaterialId)
                    .HasIndex(r => r.UserId)
                    .IsUnique();*/

        /*modelBuilder.Entity<Comment>()
                    .Property(c => c.Text)
                    .HasMaxLength(500);*/

        modelBuilder.Entity<Comment>().ToTable("Comment");


        base.OnModelCreating(modelBuilder);
    }
}

//from factory
/*public DatabaseContext CreateDbContext(string[] args)
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
    }*/