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
        MaterialController materialController) : base(logger, commentController, ratingController, materialController)
    {
        _logger = logger;
    }

    public Task<Response> DeleteMaterial(int MaterialId)
    {
        throw new NotImplementedException();
    }

    public Task<(Response, MaterialDTO)> PostMaterial(int MaterialId, MaterialCreateDTO material)
    {
        throw new NotImplementedException();
    }

    public Task<Response> PutMaterial(int MaterialId, MaterialUpdateDTO material)
    {
        throw new NotImplementedException();
    }
}
