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
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<(Status, DetailsMaterialDTO?)> Get(int id)
    {
        var material = await _materialController.ReadMaterial(id);
        if(material == null)
        {
            return (Status.NotFound, null);
        }

        var commentsDTOs = await _commentController.ReadAllComments(id);
        var computedRating = await _ratingController.ComputeRating(id);
        // var tags = await _tagController.ReadAllTags(id); 
        var tags = new List<TagDTO>(); // remove
        
        
        var materialDTO = new DetailsMaterialDTO(
            material.Id,
            material.AuthorId,
            material.Name,
            material.Description,
            material.FileType,
            material.FilePath,     
            tags, // tags
            commentsDTOs.ToList(),
            computedRating.rating
        );
        return (Status.Created, materialDTO);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("{MaterialId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<(Status status, CommentDTO comment)> PathComment(CreateCommentDTO comment)
    {
        return _commentController.CreateComment(comment);
    }
    [AllowAnonymous]
    [HttpPatch("{MaterialID}")] // find a different way to have two patch way methods

    public Task<Status> PatchRating(int MaterialID, RatingDTO rating)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    [HttpGet]
    public Task<(Status, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput)
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Status> DeleteComment(int commentId)
    {
        DeleteComment(commentid);
    }
}