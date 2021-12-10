namespace SE_training.Core;

public interface IUserRepository
{
    Task<(Status status, UserDTO? user)> CreateAsync(CreateUserDTO user);
    Task<UserDTO> ReadAsync(int userId);
    Task<UserDTO> ReadAsync(string userName);
    Task<IReadOnlyCollection<UserDTO>> ReadAllAsync();
    Task<Status> DeleteAsync(int userId);
}