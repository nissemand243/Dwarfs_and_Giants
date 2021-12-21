namespace SE_training.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ModeratorController : BasicController//, IModeratorController
{
    private readonly ILogger<ModeratorController> _logger;
    public ModeratorController(
        ILogger<ModeratorController> logger,
        CommentController commentController,
        RatingController ratingController,
        MaterialController materialController,
        SearchEngine searchEngine) : base(logger, commentController, ratingController, materialController, searchEngine)
    {
        _logger = logger;
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteMaterial(int MaterialId)
    {
        var status = await _materialController.DeleteMaterial(MaterialId);
        Status commentStatus, ratingStatus;
        
        if(status == Status.Deleted)
        {
            commentStatus =_commentController.DeleteAllComments(MaterialId).Result;
            ratingStatus = _ratingController.DeleteAllRatings(MaterialId).Result;
        } else{
            return NotFound();
        }

        return Ok();
    }
    
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
    [HttpDelete("Comment/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteComment(int commentId)
    {
        var response = await _commentController.DeleteComment(commentId);
        if (response == Deleted)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> PostMaterial(CreateMaterialDTO material)
    {
        var created = await _materialController.CreateMaterial(material);
        if(created.status != Status.Created)
        {
            return BadRequest();
        }
       
        return CreatedAtAction(nameof(Get), new { created.material.Id }, created); 
    }

    [Authorize(Roles = $"{Roles.Teacher},{Administrator}")] 
    [HttpPost("{MaterialId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> PutMaterial(int MaterialId, MaterialDTO material)
    {
        var respons = await _materialController.UpdateMaterial(MaterialId, material);
       if(respons == Status.Updated) {
           return Ok(); 
        }
        else
        {
            return NotFound();
        }
    }
}