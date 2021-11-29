namespace Core;

public interface IAPIControllerBase
{
    Task<Status> PatchComment(int id, CommentDTO comment);
    Task<Status> PatchRating(int id, RatingDTO rating);
    Task<(Status, MaterialDTO)> Get(int id);
    Task<(Status, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput);
}
