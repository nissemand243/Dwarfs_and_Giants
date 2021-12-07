
namespace SE_training.Server.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _repository;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, CommentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<Status> DeleteComment(int commentId)
        {
            var status = _repository.DeleteAsync(commentId);

            return status;
        }

        public Task<IReadOnlyCollection<CommentDTO>> ReadAll(int materialId)
        {
            return _repository.GetAsync(materialId);
        }

        public Task<(Status status, CommentDTO Comment)> CreateComment(CreateCommentDTO comment)
        {
            var response = _repository.PutAsync(comment);
            return response;
        }

        public Task<(Status status, CommentDTO comment)> UpdateComment(CreateCommentDTO comment)
        {
            return _repository.PutAsync(comment);
        }    
    }
}