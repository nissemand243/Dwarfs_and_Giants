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
        var ratingId = await GetRatingIdIfExists(rating.MaterialId, rating.UserId);
        if(ratingId != -1)
        {
            var ratingDTO = new RatingDTO(ratingId,rating.MaterialId,rating.UserId,rating.Value);
            var status = await UpdateAsync(ratingDTO);
            return(status, ratingDTO);
            
        } 
        else
        {
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

    private async Task<int> GetRatingIdIfExists(int materialId, int userId)
    {
        var rating = await _context.Ratings
                .Where(r => r.MaterialId == materialId)
                .Where(r => r.UserId == userId)
                .Select(r => new RatingDTO
                (
                    r.Id, 
                    r.MaterialId, 
                    r.UserId, 
                    r.Value
                ))            
                .SingleOrDefaultAsync();
        if(rating == null)
        {
            return -1;
        }
        return rating.Id;
    }
}