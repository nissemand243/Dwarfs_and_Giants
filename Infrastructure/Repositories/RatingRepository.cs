namespace SE_training.Infrastructure;

public class RatingRepository : IRatingRepository
{
    private readonly IDatabaseContext _context;

    public RatingRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, RatingDTO rating)> CreateAsync(CreateRatingDTO rating)
    {
        var exists = await _context.Ratings
                .Where(r => r.MaterialId == rating.MaterialId)
                .Where(r => r.UserId == rating.UserId)
                .Select(r => new RatingDTO(r.Id, r.MaterialId, r.UserId, r.Value))            
                .SingleOrDefaultAsync();
        if(exists != null)
        {   var update = new RatingDTO(exists.Id, exists.MaterialId, exists.UserId, rating.Value);
            var status = await UpdateAsync(update);
            return (status, update);
        }
        
        var entity = new Rating()
        {
            MaterialId = rating.MaterialId,
            UserId = rating.UserId,
            Value = rating.Value            
        };
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

    public async Task<Status> DeleteAllAsync(int materialId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.MaterialId == materialId)
            .Select(r => r)
            .ToListAsync();

        if (! ratings.Any())
        {
            return NotFound; 
        }

        foreach (var rating in ratings)
        {
            _context.Ratings.Remove(rating);
        }
        await _context.SaveChangesAsync();
        
        return Deleted;
    }
}