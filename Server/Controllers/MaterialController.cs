namespace SE_training.Server.Controllers

{
    public class MaterialController : ControllerBase
    {
        private readonly MaterialRepository _repository;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(ILogger<MaterialController> logger, MaterialRepository repository) 
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<MaterialDTO> ReadMaterial(int materialId)
        {
            return _repository.ReadAsync(materialId);
        }

        public Task<Status> DeleteMaterial(int materialId)
        {
            return _repository.DeleteAsync(materialId);
        }

        public async Task<(Status, MaterialDTO)> CreateMaterial(CreateMaterialDTO materialDTO)
        {
            return await _repository.PutAsync(materialDTO);   
        }
        

        
    }
}