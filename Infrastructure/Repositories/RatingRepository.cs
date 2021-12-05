namespace SE_training.Infrastructure;

public class RatingRepository : IRatingRepository
{
    readonly DatabaseContext context;

    public RatingRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status, RatingDTO)> PutAsync(CreateRatingDTO rating)
    {
        var entity = new Rating
        {
            MaterialId = rating.MaterialId,
            UserId = rating.UserId,
            Value = rating.Value
        };
        context.Ratings.Add(entity);
        await context.SaveChangesAsync();

        var details = new RatingDTO(entity.RatingId, entity.MaterialId, entity.UserId, entity.Value);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<RatingDTO>> GetAsync(int MaterialId)
    {
        var ratings = from r in context.Ratings
                      where r.MaterialId == MaterialId
                      select new RatingDTO(r.RatingId, r.MaterialId, r.UserId, r.Value);

        return await ratings.ToListAsync();
    }

    public async Task<IReadOnlyCollection<RatingDTO>> GetAsync()
    {
        return (await context.Ratings
                             .Select(r => new RatingDTO(r.RatingId, r.MaterialId, r.UserId, r.Value))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> PostAsync(RatingDTO rating)
    {
        var entity = await context.Ratings.FindAsync(rating.RatingId);

        if (entity == null) return NotFound;

        entity.Value = rating.Value;
        await context.SaveChangesAsync();
        return Updated;
    }

    public async Task<Status> DeleteAsync(int RatingId)
    {
        var entity = await context.Ratings.FindAsync(RatingId);

        if (entity == null) return NotFound;

        context.Ratings.Remove(entity);
        await context.SaveChangesAsync();

        return Deleted;
    }
}