namespace SE_training.Infrastructure;

public class RatingRepository : IRatingRepository
{
    public Task<(Status, RatingDTO)> CreateAsync(RatingCreateDTO Rating)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int RatingId)
    {
        throw new NotImplementedException();
    }

    public Task<RatingDTO> ReadAsync(int RatingId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<RatingDTO>> ReadAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(RatingDTO Rating)
    {
        throw new NotImplementedException();
    }
}