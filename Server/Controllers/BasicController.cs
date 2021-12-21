namespace SE_training.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class BasicController : ControllerBase
{
    private readonly ILogger<BasicController> _logger;
    protected readonly IUserRepository _userRepo;
    protected readonly IMaterialRepository _materialRepo;
    protected readonly ITagRepository _tagRepo;
    protected readonly IRatingRepository _ratingRepo;
    protected readonly ICommentRepository _commentRepo;
    protected readonly SearchEngine _searchEngine;
    
    public BasicController(ILogger<BasicController> logger, IUserRepository userRepo, IMaterialRepository materialRepo, ITagRepository tagRepo, IRatingRepository ratingRepo, ICommentRepository commentRepo)
    {
        _logger = logger;

        _userRepo = userRepo;
        _materialRepo = materialRepo;
        _tagRepo = tagRepo;
        _ratingRepo = ratingRepo;
        _commentRepo = commentRepo;

        _searchEngine = new SearchEngine(userRepo, materialRepo, tagRepo, commentRepo, ratingRepo);
    }
    
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Material/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsMaterialDTO?>> Get(string MaterialID)
    {
        int id;
        if (! Int32.TryParse(MaterialID, out id)) return BadRequest();

        var material = await _searchEngine.GetDetailedMaterialByIdAsync(id);

        System.Console.WriteLine("LOOKING FOR ID " + id);
        System.Console.WriteLine(material);
        System.Console.WriteLine("END OF LIST");

        if (material == null) return NotFound();
        else return Ok(material);
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
        if (! Int32.TryParse(MaterialID, out id)) return BadRequest();

        var recommented = await _searchEngine.GetRelatedMaterialsByTagsAsync(id);
        return Ok(recommented);
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpGet("Comment/{MaterialID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<CommentDTO>>> FindCommentsMaterials(string MaterialID)
    {
        int id;
        if (! Int32.TryParse(MaterialID, out id)) return BadRequest();
        
        var comments = await _commentRepo.ReadAsync(id);
        return Ok(comments);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(CreateCommentDTO CommentInfo)
    {
        var response = await _commentRepo.CreateAsync(CommentInfo);

        if (response.status == Status.Created) return Ok();
        else return BadRequest();
    }


    [Authorize]
    [HttpPost("Rating/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostRating(CreateRatingDTO RatingDTO)
    {
        var response = await _ratingRepo.CreateAsync(RatingDTO);

        if (response.status == Status.Created || response.status == Status.Updated) return Ok();
        else return BadRequest();
    }
}