namespace SE_training.Infrastructure;

public class RatingRepository : IRatingRepository
{
    readonly DatabaseContext context;

    public RatingRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status status, RatingDTO rating)> PutAsync(CreateRatingDTO rating)
    {
        var entity = new Rating
        {
            MaterialId = rating.MaterialId,
            UserId = rating.UserId,
            Value = rating.Value
        };
        context.Ratings.Add(entity);
        await context.SaveChangesAsync();

        var details = new RatingDTO(entity.Id, entity.MaterialId, entity.UserId, entity.Value);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<RatingDTO>> GetAsync(int materialId)
    {
        var ratings = from r in context.Ratings
                      where r.MaterialId == materialId
                      select new RatingDTO(r.Id, r.MaterialId, r.UserId, r.Value);

        return await ratings.ToListAsync();
    }

    public async Task<IReadOnlyCollection<RatingDTO>> GetAsync()
    {
        return (await context.Ratings
                             .Select(r => new RatingDTO(r.Id, r.MaterialId, r.UserId, r.Value))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> PostAsync(RatingDTO rating)
    {
        var entity = await context.Ratings.FindAsync(rating.Id);

        if (entity == null) return NotFound;

        entity.Value = rating.Value;
        await context.SaveChangesAsync();
        return Updated;
    }

    public async Task<Status> DeleteAsync(int ratingId)
    {
        var entity = await context.Ratings.FindAsync(ratingId);

        if (entity == null) return NotFound;

        context.Ratings.Remove(entity);
        await context.SaveChangesAsync();

        return Deleted;
    }
}