

namespace SE_training.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]

public class APIControllerBase : ControllerBase 
{
    internal readonly CommentController _commentController;
    internal readonly RatingController _ratingController;
    internal readonly MaterialController _materialController;
    
    private readonly ILogger<APIControllerBase> _logger;

    public APIControllerBase(ILogger<APIControllerBase> logger, CommentController commentController, RatingController ratingController, MaterialController materialController)
    {
        _logger = logger;
        _commentController = commentController;
        _ratingController = ratingController;
        _materialController = materialController;
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    // public async Task<(Status, DetailsMaterialDTO?)> Get(int id)
    // {
    //     var material = await _materialController.ReadMaterial(id);
    //     if(material == null)
    //     {
    //         return (NotFound, null);
    //     }

    //     var commentsDTOs = await _commentController.ReadAllComments(id);
    //     var computedRating = await _ratingController.ComputeRating(id);
    //     // var tags = await _tagController.ReadAllTags(id); 
    //     var tags = new List<TagDTO>(); // remove
        
        
    //     var materialDTO = new DetailsMaterialDTO(
    //         material.Id,
    //         material.AuthorId,
    //         material.Name,
    //         material.Description,
    //         material.FileType,
    //         material.FilePath,     
    //         tags, // tags
    //         commentsDTOs.ToList(),
    //         computedRating.rating
    //     );
    //     return (Created, materialDTO);
    // }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<DetailsMaterialDTO>), StatusCodes.Status200OK)]
    [HttpGet("{SearchString}")]
    public async Task<ActionResult<List<DetailsMaterialDTO>>> GetSearchMaterial(string searchString)
        => throw new NotImplementedException();





//***********************************THIS ONE
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet("{Test}")]
   public async Task<ActionResult<TagDTO>> GetTest(TagDTO Test)
    {
        var ting =  new TagDTO(2,3,Test.TagName);
        return Ok("Sut");
    }




    
    [AllowAnonymous]
    [HttpPatch("{MaterialID}")]


    public Task<Status> PatchComment(int MaterialId, CommentDTO comment)
    {
        throw new NotImplementedException();
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
}
