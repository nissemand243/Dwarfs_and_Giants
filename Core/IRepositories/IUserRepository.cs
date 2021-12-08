namespace SE_training.Core;

public interface IUserRepository
{
    Task<(Status status, UserDTO user)> PutAsync(CreateUserDTO user);
    Task<UserDTO> GetAsync(int userId);
    Task<UserDTO> GetAsync(string userName);
    Task<IReadOnlyCollection<UserDTO>> GetAsync();
    Task<Status> DeleteAsync(int userId);
}