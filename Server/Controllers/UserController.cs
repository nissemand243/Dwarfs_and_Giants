namespace SE_training.Server.Controllers
{
    public class UserAuthController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserAuthController(IUserRepository repository)
        {
            _repository = repository;
        }

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