namespace SE_training.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ModeratorController : BasicController
{
    private readonly ILogger<ModeratorController> _logger;

    public ModeratorController(ILogger<ModeratorController> logger, IUserRepository userRepo, IMaterialRepository materialRepo, ITagRepository tagRepo, IRatingRepository ratingRepo, ICommentRepository commentRepo) : base(logger, userRepo, materialRepo, tagRepo, ratingRepo, commentRepo)
    {
        _logger = logger;
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteMaterial(int MaterialId)
    {
        var materialStatus = await _materialRepo.DeleteAsync(MaterialId);
        if (materialStatus != Status.Deleted) return BadRequest();

        var tagStatus = await _tagRepo.DeleteAllAsync(MaterialId);
        var ratingStatus = await _ratingRepo.DeleteAllAsync(MaterialId);
        var commentStatus = await _commentRepo.DeleteAllAsync(MaterialId);

        if (tagStatus != Status.Deleted || 
            ratingStatus != Status.Deleted || 
            commentStatus != Status.Deleted)
                return Conflict();
        else return Ok();
    }

    [Authorize(Roles = $"{Roles.Teacher},{Roles.Administrator}")]
    [HttpDelete("Comment/{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteComment(string commentId)
    {
        int id;
        if (! Int32.TryParse(commentId, out id)) return BadRequest();

        var status = await _commentRepo.DeleteAsync(id);

        if (status == Status.Deleted) return Ok();
        else return NotFound();
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetailsMaterialDTO>> PostMaterial(CreateMaterialDTO material)
    {
        var response = await _materialRepo.CreateMaterialAsync(material);
        if (response.status != Status.Created) return BadRequest();

        var detailed = await _searchEngine.GetDetailedMaterialByIdAsync(response.material.Id);
        
        return Ok(detailed);
    }
}