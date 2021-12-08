namespace SE_training.Core;

public interface IRatingRepository
{
    Task<(Status status, RatingDTO rating)> CreateAsync(CreateRatingDTO rating);
    Task<IReadOnlyCollection<RatingDTO>> ReadAsync(int materialId);
    Task<IReadOnlyCollection<RatingDTO>> ReadAsync();
    Task<Status> UpdateAsync(RatingDTO rating);
    Task<Status> DeleteAsync(int ratingId);
}