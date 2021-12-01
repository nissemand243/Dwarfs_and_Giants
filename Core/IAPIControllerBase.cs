namespace SE_training.Core;

public interface IAPIControllerBase
{
    Task<Response> PatchComment(int id, CommentDto comment);
    Task<Response> PatchRating(int id, RatingDto rating);
    Task<(Response, MaterialDto)> Get(int id);
    Task<(Response, IReadOnlyCollection<MaterialDto>)> Search(string searchInput);
}
