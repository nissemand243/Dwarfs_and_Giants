namespace SE_training.Server.Controllers

{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository _repository;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(ILogger<MaterialController> logger, IMaterialRepository repository) 
        {
            _logger = logger;
            _repository = repository;
        }


         [Authorize]
         [HttpGet("{SearchString}")]
         public async Task<ActionResult<List<DetailsMaterialDTO>>> GetSearchMaterial(string SearchString)
        {  
            throw new NotImplementedException();
            // var List = await // Material database call
            // return Ok(List);
        }
        
        [Authorize]
        [HttpGet("Material/{MaterialID}")]
        public async Task<ActionResult<List<DetailsMaterialDTO>>> GetMaterialRecomended(int MaterialID)
        {  
            throw new NotImplementedException();
            // var List = await // Material database call
            // return Ok(List);
        }

        public Task<MaterialDTO> ReadMaterial(int materialId)
        {
            return _repository.ReadAsync(materialId);
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
