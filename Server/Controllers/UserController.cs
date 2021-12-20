namespace SE_training.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = $"{Roles.Teacher},{Roles.Student},{Roles.Administrator},{Roles.User}")]
        [HttpPost("/create{CreateUserDTO}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<(Status status, UserDTO user)> CreateUser(CreateUserDTO user)
        {
           return await _repository.CreateAsync(user);
        }

        public async Task<UserDTO> ReadAsync(string userEmail)
        {
            return await _repository.ReadAsync(userEmail);
        }
    }
}