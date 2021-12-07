namespace SE_training.Server.Controllers
{
    public class RatingController : ControllerBase
    {
        private readonly RatingRepository _repository;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger, RatingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<Status> DeleteRating(int ratingID)
        {
            return _repository.DeleteAsync(ratingID);
        }

        public Task<Status> CreateRating(RatingDTO rating)
        {
            return _repository.PostAsync(rating);
        } 

        public Task<IReadOnlyCollection<RatingDTO>> ReadAllRatings(int MaterialId)
        {
            return _repository.GetAsync(MaterialId);
        }

        public Task<(Status status, RatingDTO Rating)> UpdateRating(CreateRatingDTO rating)
        {
            return _repository.PutAsync(rating);
        }

    }
}