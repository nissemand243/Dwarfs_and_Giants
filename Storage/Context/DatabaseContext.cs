namespace Context;

public class DatabaseContext : DbContext
{
    public DbSet<MaterialDTO> Materials => Set<MaterialDTO>();
    public DbSet<UserDTO> Users => Set<UserDTO>();
    public DbSet<CommentDTO> Comments => Set<CommentDTO>();
    public DbSet<RatingDTO> Ratings => Set<RatingDTO>();
    public DbSet<TagDTO> Tags => Set<TagDTO>();

    public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Character>()
            .Property(e => e.Gender)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<Gender>());

        modelBuilder.Entity<City>()
                    .HasIndex(s => s.Name)
                    .IsUnique();

        modelBuilder.Entity<Power>()
                    .HasIndex(p => p.Name)
                    .IsUnique();
    }*/
}