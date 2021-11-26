namespace Database;

public class DatabaseContext : DbContext
{
    public DbSet<MaterialDTO> Materials => Set<MaterialDTO>();
    public DbSet<UserDTO> Users => Set<UserDTO>();
    public DbSet<CommentDTO> Comments => Set<CommentDTO>();
    public DbSet<RatingDTO> Ratings => Set<RatingDTO>();
    public DbSet<TagDTO> Tags => Set<TagDTO>();
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}