namespace SE_training.Server.Controllers

{
    internal class MaterialController
    {
        private readonly MaterialRepository _repository;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(ILogger<MaterialController> logger, MaterialRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}