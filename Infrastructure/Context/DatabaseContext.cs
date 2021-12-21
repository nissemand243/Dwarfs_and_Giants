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
        modelBuilder.Entity<User>()
                    .HasIndex(u => u.Id)
                    .IsUnique();
                    
        modelBuilder.Entity<Material>()
                    .HasIndex(u => u.Id)
                    .IsUnique();

        modelBuilder.Entity<Tag>()
                    .HasIndex(u => u.Id)
                    .IsUnique();

        modelBuilder.Entity<Rating>()
                    .HasIndex(u => u.Id)
                    .IsUnique();

        modelBuilder.Entity<Comment>()
                    .HasIndex(u => u.Id)
                    .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
