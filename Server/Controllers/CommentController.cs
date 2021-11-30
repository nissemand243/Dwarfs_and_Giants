namespace SE_training.Server.Controllers
{
    internal class CommentController : ControllerBase
    {
        private readonly CommentRepository _repository;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, CommentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}