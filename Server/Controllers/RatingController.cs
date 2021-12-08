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

        public Task<(Status, RatingDTO)> UpdateRating(CreateRatingDTO rating)
        {
            return _repository.PutAsync(rating);
        }

        public async Task<Status> DeleteAllRatings(int materialId)
        {
            var status = Status.NotFound;
            var ratings = _repository.GetAsync(materialId);

            if(!ratings.Result.Any())
            {
                return status;
            }

            foreach (var rating in ratings.Result)
            {
                status = await _repository.DeleteAsync(rating.Id);
            }
            return status;
        }

    }
}