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

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<Status> DeleteMaterial(int MaterialId)
    {
        var status =_materialController.DeleteMaterial(MaterialId);
        Status commentStatus, ratingStatus;
        
        if(status.Result == Status.Deleted)
        {
            commentStatus =_commentController.DeleteAllComments(MaterialId).Result;
            ratingStatus = _ratingController.DeleteAllRatings(MaterialId).Result;
        } else{
            return Status.BadRequest;
        }

        return await status;
        
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<(Status, MaterialDTO?)> PostMaterial(CreateMaterialDTO material)
    {
        var created = await _materialController.CreateMaterial(material);
        if(created.status != Status.Created)
        {
            return (Status.BadRequest, null); // fy fy?
        }
       
        return (created.status, created.material); 
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")] 
    [HttpPost("{MaterialId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<Status> PutMaterial(int MaterialId, MaterialDTO material)
    {
        return _materialController.UpdateMaterial(MaterialId, material);
    }
}
