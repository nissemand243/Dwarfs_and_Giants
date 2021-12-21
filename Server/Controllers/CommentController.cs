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

        public async Task<(Status status,CommentDTO? comment)> CreateComment(CreateCommentDTO comment)
        {
            var commentCreated = await _repository.CreateAsync(comment);
            if(commentCreated.status == Status.Created)
            {
                return (Status.Created, commentCreated.comment);
            }
            return (Status.BadRequest, null);
        }

        public async Task<(Status status, IReadOnlyCollection<CommentDTO> comments)> GetMaterialComments(int materialID)
        {  
            return await _repository.ReadAsync(materialID);
        }

        // Jeg er usikker på hvad der ønskes her, metoden ovenfor giver comments fra en specifikt materiale, 
        // [Authorize]
        // [HttpPost("{CommentContent}")]
        // public async Task<ActionResult> GetMaterialComments(List<string> CommentContent)
        // {  
        //     throw new NotImplementedException();
        //     // Uploade
        //     // return Ok();
        // }
   
        public async Task<Status> DeleteAllComments(int materialId)
        {
            return await _repository.DeleteAllAsync(materialId);
        }
    }
}