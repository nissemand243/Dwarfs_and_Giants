namespace SE_training.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class BasicController : ControllerBase//, IBasicController
//this fails
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
    public async Task<DetailsMaterialDTO?> Get(int id)
    {
        // Does This Work?
        var detailedDto = await _searchEngine.GetDetailedMaterialByIdAsync(id);
        return (detailedDto);
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
    public Task<(Status,IReadOnlyCollection<MaterialDTO>)> Search(string searchInput)
    {
        //Search Code HERE
        throw new NotImplementedException();
      
    }


   

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Recommended/{Id}")]
    public Task<(Status,IReadOnlyCollection<MaterialDTO>)> FindRecommendedMaterials(string Id)
    {
         throw new NotImplementedException();
        //Relatede Material Code HERE
      
    }
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Comment/{Id}")]
    public Task<IReadOnlyCollection<CommentDTO>> FindCommentsMaterials(string Id)
    {
         throw new NotImplementedException();
        //Relatede Material Code HERE
      
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