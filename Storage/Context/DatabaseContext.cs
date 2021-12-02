namespace SE_training.Context;

public class DatabaseContext : DbContext, IDatabseContext
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
                    .HasIndex(u => u.Name)
                    .IsUnique();

        /*modelBuilder.Entity<Material>()
                    .Property(e => e.Gender)
                    .HasMaxLength(50)
                    .HasConversion(new EnumToStringConverter<Gender>());

        modelBuilder.Entity<Tag>()
                    .HasIndex(p => p.Name)
                    .IsUnique();

        modelBuilder.Entity<Rating>()
                    .HasIndex(p => p.Name)
                    .IsUnique();

        modelBuilder.Entity<Comment>()
                    .HasIndex(p => p.Name)
                    .IsUnique();*/
    }
}