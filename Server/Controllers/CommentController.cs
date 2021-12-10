namespace SE_training.Server.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, ICommentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<Status> DeleteComment(int commentId)
        {
            return _repository.DeleteAsync(commentId);
        }

        public Task<IReadOnlyCollection<CommentDTO>> ReadAllComments(int materialId)
        {
            return _repository.ReadAsync(materialId);
        }
       
        public async Task<(Status status,CommentDTO comment)> CreateComment(CreateCommentDTO comment)
        {
            var commentCreated = await _repository.CreateAsync(comment);
            if(commentCreated.status == Status.Created)
            {
                return (Status.Created, commentCreated.comment);
            }
            return (Status.BadRequest, null);
            
           
        }
 

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