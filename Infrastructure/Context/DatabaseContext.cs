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
        modelBuilder.Entity<Teacher>();
        modelBuilder.Entity<Student>();

        modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

        //modelBuilder.Entity<User>().ToTable("User");
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
