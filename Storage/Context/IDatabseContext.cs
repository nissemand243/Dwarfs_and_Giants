namespace Context;

public interface IDatabseContext : IDisposable
{
    DbSet<Comment> Comments { get; }
    DbSet<Material> Materials { get; }
    DbSet<Tag> Tags { get; }
    DbSet<Rating> Ratings { get; }
    DbSet<User> Users { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}