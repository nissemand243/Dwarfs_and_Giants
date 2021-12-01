namespace SE_training.Infrastructure;

public interface ISETrainingContext : IDisposable
    {
        DbSet<Material> Materials {get;}
        DbSet<User> Users {get;}
        DbSet<Comment> Comments {get;}
        DbSet<Rating> Ratings {get;}
        DbSet<Tag> Tags {get;}
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
