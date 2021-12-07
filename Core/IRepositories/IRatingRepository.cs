namespace SE_training.Core;

public interface IRatingRepository
{
    Task<(Status status, RatingDTO rating)> PutAsync(CreateRatingDTO rating);
    Task<IReadOnlyCollection<RatingDTO>> GetAsync(int materialId);
    Task<IReadOnlyCollection<RatingDTO>> GetAsync();
    Task<Status> PostAsync(RatingDTO rating);
    Task<Status> DeleteAsync(int ratingId);
}