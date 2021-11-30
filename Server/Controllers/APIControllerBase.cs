

namespace SE_training.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class APIControllerBase : ControllerBase, IAPIControllerBase
{
    private readonly ILogger<APIControllerBase> _logger;
    internal readonly UserAPI _acccesPoint;
    public APIControllerBase(ILogger<APIControllerBase> logger, UserAPI accessPoint)
    {
        _logger = logger;
        _acccesPoint = accessPoint;
    }
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
