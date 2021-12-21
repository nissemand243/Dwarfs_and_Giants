namespace SE_training.Server.Controllers
{
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _repository;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger, IRatingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<Status> DeleteRating(int ratingID)
        {
            return _repository.DeleteAsync(ratingID);
        }

        public Task<(Status status, RatingDTO rating)> CreateRating(CreateRatingDTO rating)
        {
            return _repository.CreateAsync(rating);
        } 

        public Task<IReadOnlyCollection<RatingDTO>> ReadAllRatings(int MaterialId)
        {
            return _repository.ReadAsync(MaterialId);
        }

        public async Task<Status> DeleteAllRatings(int materialId) => await _repository.DeleteAllAsync(materialId);
    }
}