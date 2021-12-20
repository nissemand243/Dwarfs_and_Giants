namespace SE_training.Core;

public interface IUserRepository
{
    Task<(Status status, UserDTO user)> CreateAsync(CreateUserDTO user);
    Task<UserDTO> ReadAsyncId(int userId);
    Task<UserDTO> ReadAsync(string userEmail);
    Task<IReadOnlyCollection<UserDTO>> ReadAllAsync();
}