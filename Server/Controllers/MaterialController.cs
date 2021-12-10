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
            return _repository.GetAsync(materialId);
        }

        public Task<Status> DeleteMaterial(int materialId)
        {
            return _repository.DeleteAsync(materialId);
        }

        public async Task<(Status status, MaterialDTO material)> CreateMaterial(CreateMaterialDTO materialDTO)
        {
            return await _repository.CreateMaterial(materialDTO);   
        }

        public async Task<Status> UpdateMaterial(int materialId, MaterialDTO material)
        {
            return await _repository.UpdateMaterial(materialId, material);
        }
        

        
    }
}