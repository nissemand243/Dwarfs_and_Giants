namespace SE_training.Repositories;

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
            MaterialId = rating.materialId,
            UserId = rating.userId,
            Value = rating.value
        };
        context.Ratings.Add(entity);
        await context.SaveChangesAsync();

        var details = new RatingDTO(entity.RatingId, entity.MaterialId, entity.UserId, entity.Value);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<RatingDTO>> GetAsync(int materialId)
    {
        var ratings = from r in context.Ratings
                    where r.MaterialId == materialId
                    select new RatingDTO(r.RatingId, r.MaterialId, r.UserId, r.Value);

        return await ratings.ToListAsync();
    }

    public async Task<IReadOnlyCollection<RatingDTO>> GetAsync()
    {
        return (await context.Ratings
                             .Select(r => new RatingDTO(r.RatingId, r.MaterialId, r.UserId, r.Value))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> PostAsync(RatingDTO rating){
        var entity = await context.Ratings.FindAsync(rating.ratingId);

        if (entity == null) return NotFound;

        entity.Value = rating.value;
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