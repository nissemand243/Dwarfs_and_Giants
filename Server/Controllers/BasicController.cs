namespace SE_training.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class BasicController : ControllerBase, IBasicController
{
    internal readonly CommentController _commentController;
    internal readonly RatingController _ratingController;
    internal readonly MaterialController _materialController;

    internal readonly ISEarchEngine _searchEngine;
    private readonly ILogger<BasicController> _logger;

    public BasicController(ILogger<BasicController> logger, CommentController commentController, RatingController ratingController, 
    MaterialController materialController, SearchEngine searchEngine)
    {
        _logger = logger;
        _commentController = commentController;
        _ratingController = ratingController;
        _materialController = materialController;
        _searchEngine = searchEngine;
    }
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Material/{MaterialID}")]
    public async Task<(Status, DetailsMaterialDTO?)> Get(int id)
    {
        var detailedDto = await _searchEngine.GetDetailedMaterialByIdAsync(id);
        if(detailedDto == null)
        {
            return(Status.NotFound, null);
        }
        return (Status.Found, detailedDto);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("Material/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<(Status status, CommentDTO comment)> PatchComment(CreateCommentDTO comment)
    {
        return _commentController.CreateComment(comment);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("{MaterialID}")]
    public Task<(Status status, RatingDTO rating)> PatchRating(CreateRatingDTO rating)
    {
        return _ratingController.CreateRating(rating);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("{SearchString}")]
    public Task<IReadOnlyCollection<MaterialDTO>> Search(string searchInput)
    {
        return _searchEngine.SearchAsync(searchInput);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpDelete("specifikMaterial/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Status> DeleteComment(int commentId)
    {
        return await _commentController.DeleteComment(commentId);
    }
}