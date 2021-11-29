namespace SE_training.Server.Controllers;

public class APIControllerBase : IAPIControllerBase
{
    public Task<(Response, MaterialDTO)> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response> PatchComment(int id, CommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task<Response> PatchRating(int id, RatingDTO rating)
    {
        throw new NotImplementedException();
    }

    public Task<(Response, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput)
    {
        throw new NotImplementedException();
    }
}
