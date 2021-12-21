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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsMaterialDTO?>> Get(int id)
    {
        // Does This Work? -- Yes i hope
        var detailedDto = await _searchEngine.GetDetailedMaterialByIdAsync(id);
        if (detailedDto != null)
        {
            return (detailedDto);
        }
        return NotFound();

    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPost("Material/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommentDTO>> PostComment(CreateCommentDTO comment)
    {
        var response = await _commentController.CreateComment(comment);
        if(response.status == Status.Created)
        {
            return CreatedAtAction(nameof(Get), new { response.comment.Id }, response); 
        }
        else{
            return BadRequest();
        }
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RatingDTO>> PatchRating(CreateRatingDTO rating)
    {
        var response = await _ratingController.CreateRating(rating);
        if(response.status == Status.Updated)
        {
            return response.rating;
        }
        else
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("{SearchString}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IReadOnlyCollection<MaterialDTO>> Search(string searchInput)
    {
        var materials = await _searchEngine.SearchAsync(searchInput);
        return materials;     
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Recommended/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IReadOnlyCollection<MaterialDTO>> FindRecommendedMaterials(int Id)
    {
        var RecommendedMaterial = await _searchEngine.GetRelatedMaterialsByTagsAsync(Id);
        return RecommendedMaterial;
      
    }
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Comment/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<(Status status, IReadOnlyCollection<CommentDTO>? comments)> GetCommentMaterials(int materialId)
    {

        var response = await _commentController.GetMaterialComments(materialId);
        if(response.status == Found)
        {
            return (Found, response.comments);
        } 
        else
        {
            return (Status.NotFound, null);
        }
    }

}