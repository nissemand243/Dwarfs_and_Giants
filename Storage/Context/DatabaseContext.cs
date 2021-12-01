using SE_training.Infrastrucre;

namespace Context;

public class DatabaseContext : DbContext, ISETrainingContext
{
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Tag> Tags => Set<Tag>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        // implement 
    }

}