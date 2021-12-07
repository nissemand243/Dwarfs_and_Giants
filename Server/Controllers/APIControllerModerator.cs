using SE_training.Infrastructure;

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

    //[Authorize(User = $"Teacher")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<Status> DeleteMaterial(int MaterialId)
    {
        throw new NotImplementedException();
    }

    //[Authorize(User = $"Teacher")]
    [HttpPost]
    public Task<(Status, MaterialDTO)> PostMaterial(int MaterialId, CreateMaterialDTO material)
    {
        throw new NotImplementedException();
    }

    //[Authorize(User = $"Teacher")] 
    [HttpPost("{MaterialId}")]
    public Task<Status> PutMaterial(int MaterialId, MaterialDTO material)
    {
        throw new NotImplementedException();
    }
}
