namespace SE_training.Infrastructure;

public class RatingRepository : IRatingRepository
{
    private readonly DatabaseContext _context;

    public RatingRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, RatingDTO rating)> CreateAsync(CreateRatingDTO rating)
    {
        var entity = new Rating() {
            MaterialId = rating.MaterialId,
            UserId = rating.UserId,
            Value = rating.Value
        };
        
        _context.Ratings.Add(entity);
        await _context.SaveChangesAsync();

        var details = new RatingDTO(entity.Id, entity.MaterialId, entity.UserId, entity.Value);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<RatingDTO>> ReadAsync(int materialId)
    {
        var ratings = from r in _context.Ratings
                      where r.MaterialId == materialId
                      select new RatingDTO(r.Id, r.MaterialId, r.UserId, r.Value);

        return await ratings.ToListAsync();
    }

    public async Task<IReadOnlyCollection<RatingDTO>> ReadAsync()
    {
        return (await _context.Ratings
                             .Select(r => new RatingDTO(r.Id, r.MaterialId, r.UserId, r.Value))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> UpdateAsync(RatingDTO rating)
    {
        var entity = await _context.Ratings.FindAsync(rating.Id);

        if (entity == null) return NotFound;

        entity.Value = rating.Value;
        await _context.SaveChangesAsync();
        return Updated;
    }

    public async Task<Status> DeleteAsync(int ratingId)
    {
        var entity = await _context.Ratings.FindAsync(ratingId);

        if (entity == null) return NotFound;

        _context.Ratings.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }
}