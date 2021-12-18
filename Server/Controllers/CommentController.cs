namespace SE_training.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, ICommentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<Status> DeleteComment(int commentId)
        {
            return _repository.DeleteAsync(commentId);
        }

        [Authorize(Roles = $"{Roles.Teacher},{Roles.Administrator}")]
        public Task<IReadOnlyCollection<CommentDTO>> ReadAllComments(int materialId)
        {
            return _repository.ReadAsync(materialId);
        }
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<(Status status,CommentDTO comment)> CreateComment(CreateCommentDTO comment)
        {
            var commentCreated = await _repository.CreateAsync(comment);
            if(commentCreated.status == Status.Created)
            {
                return (Status.Created, commentCreated.comment);
            }
            return (Status.BadRequest, null);
            
           
        }
 

        [Authorize]
        [HttpGet("Comments/{MaterialID}")]
        public async Task<ActionResult<List<CommentDTO>>> GetMaterialComments(int MaterialID)
        {  
            throw new NotImplementedException();
            // var List = await // Material database call
            // return Ok(List);
        }

  


   
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Administrator}")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Status> DeleteAllComments(int materialId)
        {
            var status = Status.NotFound;
            var comments = _repository.ReadAsync(materialId);

            if(!comments.Result.Any())
            {
                return status; 
            }
            
            foreach (var comment in comments.Result)
            { 
                status = await _repository.DeleteAsync(comment.Id);
            }
            return status;
        }
    }
}