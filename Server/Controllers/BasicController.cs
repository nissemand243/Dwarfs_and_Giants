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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsMaterialDTO?>> Get(int id)
    {
        // Does This Work?
        var detailedDto = await _searchEngine.GetDetailedMaterialByIdAsync(id);
        if (detailedDto != null)
        {
            return (detailedDto);
        }
        return NotFound();

    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("Material/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommentDTO>> PatchComment(CreateCommentDTO comment)
    {
        var created = await _commentController.CreateComment(comment);
        if(created.status == Status.Created)
        {
            return CreatedAtAction(nameof(Get), new { created.comment.Id }, created); 
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
    public Task<ActionResult<IReadOnlyCollection<MaterialDTO>>> Search(string searchInput)
    {
        //Search Code HERE
        throw new NotImplementedException();
      
    }


   

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Recommended/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<IReadOnlyCollection<MaterialDTO>>> FindRecommendedMaterials(string Id)
    {
         throw new NotImplementedException();
        //Relatede Material Code HERE
      
    }
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Comment/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<IReadOnlyCollection<CommentDTO>>> FindCommentsMaterials(string Id)
    {
         throw new NotImplementedException();
        //Relatede Material Code HERE
      
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(List<string> CommentInfo)
    {

         throw new NotImplementedException();
       // CommentInfo[0] = materialId.. CommentInfo[1] = decription
       return Ok();
       


    }

}