namespace SE_training.IRepositories;

public interface IRatingRepository
{
    Task<(Status, RatingDTO)> PutAsync(CreateRatingDTO rating);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync(int materialId);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync();
    Task<Status> PostAsync(CreateRatingDTO rating);
    Task<Status> DeleteAsync(int ratingId);
}