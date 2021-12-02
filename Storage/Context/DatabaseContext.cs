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
                    .HasIndex(u => u.Email)
                    .IsUnique();

        modelBuilder.Entity<Material>();

        /*modelBuilder.Entity<Tag>()
                    .HasIndex(t => t.MaterialId)
                    .HasIndex(t => t.TagName)
                    .IsUnique();

        modelBuilder.Entity<Rating>()
                    .HasIndex(r => r.MaterialId)
                    .HasIndex(r => r.UserId)
                    .IsUnique();*/

        modelBuilder.Entity<Comment>()
                    .Property(c => c.Text)
                    .HasMaxLength(500);
    }
}