namespace SE_training.Core;

public interface IAPIControllerBase
{
    Task<Status> PatchComment(int id, CommentDTO comment);
    Task<Status> PatchRating(int id, RatingDTO rating);
    Task<(Status, DetailsMaterialDTO)> Get(int id);
    Task<(Status, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput);
}
