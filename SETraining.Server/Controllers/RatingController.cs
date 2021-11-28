// namespace SETraining.Server.Controllers;

// using SETraining.Core;

// [Authorize]
// [ApiController]
// [Route("api/[controller]")]
// [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
// public class RatingController : ControllerBase
// {
//     private readonly ILogger<RatingController> _logger;
//     private readonly IRatingRepository _repository;

//     public RatingController(IRatingRepository repository, ILogger<RatingController> logger)
//     {
//         _repository = repository;
//         _logger = logger;
//     }

//     [AllowAnonymous]
//     [HttpGet]
//     public async Task<IReadOnlyCollection<RatingDTO>> Get()
//         => await _repository.ReadAsync();

//     // [AllowAnonymous]
//     // [ProducesResponseType(404)]
//     // [ProducesResponseType(typeof(RatingDTO), 200)]
//     // [HttpGet("{id}")]
//     // public async Task<ActionResult<RatingDTO>> Get(int id)
//     //     => (await _repository.ReadAsync(id)).ToActionResult();

//     [Authorize]
//     [HttpPost]
//     [ProducesResponseType(typeof(RatingDTO), 201)]
//     public async Task<IActionResult> Post(RatingCreateDTO Rating)
//     {
//         var created = await _repository.CreateAsync(Rating);

//         return CreatedAtRoute(nameof(Get), new { created.Id }, created);
//     }

//     [Authorize]
//     [HttpPut("{id}")]
//     [ProducesResponseType(204)]
//     [ProducesResponseType(404)]
//     public async Task<IActionResult> Put(int id, [FromBody] RatingUpdateDTO Rating)
//            => (await _repository.UpdateAsync(id, Rating)).ToActionResult();

//     [Authorize]
//     [HttpDelete("{id}")]
//     [ProducesResponseType(204)]
//     [ProducesResponseType(404)]
//     public async Task<IActionResult> Delete(int id)
//           => (await _repository.DeleteAsync(id)).ToActionResult();
// }