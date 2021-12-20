namespace SE_training.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class StudentController : ControllerBase, IStudentController
{
    internal readonly CommentController _commentController;
    internal readonly RatingController _ratingController;
    internal readonly MaterialController _materialController;

    internal readonly ISEarchEngine _searchEngine;
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger, CommentController commentController, RatingController ratingController, 
    MaterialController materialController, SearchEngine searchEngine)
    {
        _logger = logger;
        _commentController = commentController;
        _ratingController = ratingController;
        _materialController = materialController;
        _searchEngine = searchEngine;
    }
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("{id}")]
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
    [HttpPatch("{MaterialId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<(Status status, CommentDTO comment)> PatchComment(CreateCommentDTO comment)
    {
        return _commentController.CreateComment(comment);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("{MaterialID}")] // find a different way to have two patch way methods
    public Task<(Status status, RatingDTO rating)> PatchRating(CreateRatingDTO rating)
    {
        return _ratingController.CreateRating(rating);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet]
    public Task<IReadOnlyCollection<MaterialDTO>> Search(string searchInput)
    {
        return _searchEngine.SearchAsync(searchInput);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Status> DeleteComment(int commentId)
    {
        return await _commentController.DeleteComment(commentId);
    }
}