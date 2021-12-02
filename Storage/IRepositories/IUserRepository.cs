namespace IRepositories;

public interface IUserRepository
{
    Task<(Status, UserDTO)> PutAsync(CreateUserDTO user);
    Task<UserDTO> GetAsync(int userId);
    Task<UserDTO> GetAsync(string userName);
    Task<IReadOnlyCollection<UserDTO>> GetAsync();
    Task<Status> DeleteAsync(int userId);
}