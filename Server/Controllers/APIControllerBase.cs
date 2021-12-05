

namespace SE_training.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class APIControllerBase : IAPIControllerBase
{
    internal readonly CommentController _commentController;
    internal readonly RatingController _ratingController;
    internal readonly MaterialController _materialController;
    
    private readonly ILogger<APIControllerBase> _logger;

    public APIControllerBase(ILogger<APIControllerBase> logger, CommentController commentController, RatingController ratingController, MaterialController materialController)
    {
        _logger = logger;
        _commentController = commentController;
        _ratingController = ratingController;
        _materialController = materialController;
    }
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
