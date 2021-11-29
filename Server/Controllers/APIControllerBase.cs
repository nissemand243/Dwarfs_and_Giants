namespace SE_training.Server.Controllers;

public class APIControllerBase : IAPIControllerBase
{
    public Task<(Status, MaterialDTO)> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Status> PatchComment(int id, CommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task<Status> PatchRating(int id, RatingDTO rating)
    {
        throw new NotImplementedException();
    }

    public Task<(Status, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput)
    {
        throw new NotImplementedException();
    }
}
