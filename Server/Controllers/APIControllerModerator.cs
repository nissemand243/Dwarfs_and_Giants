namespace SE_training.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class APIControllerModerator : APIControllerBase, IAPIControllerModerator
{
    private readonly ILogger<APIControllerModerator> _logger;
    public APIControllerModerator(
        ILogger<APIControllerModerator> logger,
        CommentController commentController,
        RatingController ratingController,
        MaterialController materialController) : base (logger, commentController, ratingController, materialController)
    {
        _logger = logger;
    }

    public Task<Response> DeleteMaterial(int materialId)
    {
        throw new NotImplementedException();
    }

    public Task<(Response, MaterialDto)> PostMaterial(int materialId, MaterialCreateDto material)
    {
        throw new NotImplementedException();
    }

    public Task<Response> PutMaterial(int materialId, MaterialUpdateDto material)
    {
        throw new NotImplementedException();
    }
}
