namespace SE_training.Server.Controllers
{
    internal class RatingController : ControllerBase
    {
        private readonly RatingRepository _repository;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger, RatingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}