namespace SETraining.Core;

public interface IRatingRepository
{

    Task<(Status, RatingDTO)> CreateAsync(RatingCreateDTO Rating);
    Task<RatingDTO> ReadAsync(int RatingId);
    Task<IReadOnlyCollection<RatingDTO>> ReadAsync();
    Task<Status> UpdateAsync(RatingDTO Rating);
    Task<Status> DeleteAsync(int RatingId);
}