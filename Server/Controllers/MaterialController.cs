namespace SE_training.Server.Controllers

{
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
            try
            {
                var results = await _repository.ReadAllAsync();

                //var searchEngine = new SearchEngine(new UserRepository(context), new MaterialRepository(context), new TagRepository(context), new CommentRepository(context), new RatingRepository(context));
                //var results = await searchEngine.SearchAsync(SearchString);

                return Ok(results);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("START OF EXEPTION");
                System.Console.WriteLine(e);
                System.Console.WriteLine("END OF EXEPTIOPN");
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet("Material/{MaterialID}")]
        public async Task<ActionResult<List<DetailsMaterialDTO>>> GetMaterialRecomended(int MaterialID)
        {
            throw new NotImplementedException();
            //var results = await SearchEngine.INSTANCE.GetRelatedMaterialsByTagsAsync(MaterialID);
            //return Ok(results);
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
            return await _repository.CreateMaterialAsync(materialDTO);
        }

        public async Task<Status> UpdateMaterial(int materialId, MaterialDTO material)
        {
            return await _repository.UpdateMaterialAsync(materialId, material);
        }

        public async Task<IReadOnlyCollection<MaterialDTO>> ReadAllAsync()
        {
            return await _repository.ReadAllAsync();
        }      
    }
}
