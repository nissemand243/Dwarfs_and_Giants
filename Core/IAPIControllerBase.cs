namespace Core;

public interface IAPIControllerBase
{
    Task<Response> PatchComment(int id, CommentDTO comment);
    Task<Response> PatchRating(int id, RatingDTO rating);
    Task<(Response, MaterialDTO)> Get(int id);
    Task<(Response, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput);
}
