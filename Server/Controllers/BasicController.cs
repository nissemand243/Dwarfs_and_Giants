namespace SE_training.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class BasicController : ControllerBase
{
    private readonly ILogger<BasicController> _logger;
    private readonly IMaterialRepository _repository;

    private readonly SearchEngine _searchEngine;
    
    public BasicController(ILogger<BasicController> logger, IMaterialRepository repository)
    {
        _logger = logger;

        _repository = repository;

        var context = ((MaterialRepository) repository)._context;
        _searchEngine = new SearchEngine(new UserRepository(context), new MaterialRepository(context), new TagRepository(context), new CommentRepository(context), new RatingRepository(context));
    }
    
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Material/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsMaterialDTO?>> Get(string MaterialID)
    {
        int id;
        if (! Int32.TryParse(MaterialID, out id))
        {
            return BadRequest();
        }

        var material = await _searchEngine.GetDetailedMaterialByIdAsync(id);

        System.Console.WriteLine("LOOKING FOR ID " + id);
        System.Console.WriteLine(material);
        System.Console.WriteLine("END OF LIST");

        if (material == null)
        {
            return NotFound();
        }
        return Ok(material);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("Material/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommentDTO>> PatchComment(CreateCommentDTO comment)
    {
        throw new NotImplementedException();
      
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpPatch("{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RatingDTO>> PatchRating(CreateRatingDTO rating)
    {
        throw new NotImplementedException();
        // var response = await _ratingController.CreateRating(rating);
        // if(response.status == Status.Updated)
        // {
        //     return response.rating;
        // }
        // else
        // {
        //     return BadRequest();
        // }
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Search/{SearchString}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<MaterialDTO>>> Search(string SearchString)
    {
        var result = await _searchEngine.SearchAsync(SearchString);
        return Ok(result);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Recommended/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<MaterialDTO>>> FindRecommendedMaterials(string MaterialID)
    {
        int id;
        if (! Int32.TryParse(MaterialID, out id))
        {
            return BadRequest();
        }

        var recommented = await _searchEngine.GetRelatedMaterialsByTagsAsync(id);
        return Ok(recommented);
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
    public async Task<IActionResult> Post(CreateCommentDTO CommentInfo)
    {
        return Ok();
    }


    [Authorize]
    [HttpPost("Rating/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostRating(CreateRatingDTO RatingDTO)
    {
       return Ok();
    }
}