namespace SE_training.Core;

public interface IRatingRepository
{
    Task<(Status, RatingDTO)> PutAsync(CreateRatingDTO rating);
    Task<IReadOnlyCollection<RatingDTO>> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<RatingDTO>> GetAsync();
    Task<Status> PostAsync(RatingDTO rating);
    Task<Status> DeleteAsync(int RatingId);
}