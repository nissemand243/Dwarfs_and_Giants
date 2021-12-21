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
        
        
        public async Task<Status> DeleteComment(int commentId)
        {
            return await _repository.DeleteAsync(commentId);
        }

        public async Task<IReadOnlyCollection<CommentDTO>> ReadAllComments(int materialId)
        {
            return await _repository.ReadAsync(materialId);
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

        public async Task<IReadOnlyCollection<CommentDTO>> GetMaterialComments(int materialID)
        {  
            return await _repository.ReadAsync(materialID);
        }

        public async Task<Status> DeleteAllComments(int materialId)
        {
            return await _repository.DeleteAllAsync(materialId);
        }
    }
}