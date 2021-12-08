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

        public Task<(Status status, RatingDTO rating)> CreateRating(CreateRatingDTO rating)
        {
            return _repository.CreateAsync(rating);
        } 

        public Task<IReadOnlyCollection<RatingDTO>> ReadAllRatings(int MaterialId)
        {
            return _repository.ReadAsync(MaterialId);
        }

        public async Task<(Status status, double rating)> ComputeRating(int materialId)
        {
            var ratingDTOs = await _repository.ReadAsync(materialId);
            if(ratingDTOs == null)
            {
                return (Status.NotFound, -1);
            }

            var ratings = new List<int>();
            foreach (var rating in ratingDTOs)
            {
                ratings.Add(rating.Value);
            }

            return (Status.Created, ratings.Average()); 
        } 

        public async Task<Status> UpdateRating(RatingDTO rating)
        {
            return await _repository.UpdateAsync(rating);
        }

        public async Task<Status> DeleteAllRatings(int materialId)
        {
            var status = Status.NotFound;
            var ratings = _repository.ReadAsync(materialId);

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