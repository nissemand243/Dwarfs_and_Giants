
namespace SE_training.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _repository;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, CommentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
        public Task<Status> DeleteComment(int commentId)
        {
            return _repository.DeleteAsync(commentId);
        }
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
        public Task<IReadOnlyCollection<CommentDTO>> ReadAllComments(int materialId)
        {
            return _repository.ReadAsync(materialId);
        }
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
        public Task<(Status, CommentDTO)> CreateComment(CreateCommentDTO comment)
        {
            return _repository.CreateAsync(comment);
        }
        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]

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