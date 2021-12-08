
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

        public Task<IReadOnlyCollection<CommentDTO>> ReadAllComments(int materialId)
        {
            return _repository.GetAsync(materialId);
        }

        public Task<(Status, CommentDTO)> CreateComment(CreateCommentDTO comment)
        {
            var response = _repository.PutAsync(comment);
            return response;
        }

        public Task<(Status, CommentDTO)> UpdateComment(CreateCommentDTO comment)
        {
            return _repository.PutAsync(comment);
        }    

        public async Task<Status> DeleteAllComments(int materialId)
        {
            var status = Status.NotFound;
            var comments = _repository.GetAsync(materialId);

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