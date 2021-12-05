namespace SE_training.Core;

public interface IUserRepository
{
    Task<(Status, UserDTO)> PutAsync(CreateUserDTO user);
    Task<UserDTO> GetAsync(int UserId);
    Task<UserDTO> GetAsync(string userName);
    Task<IReadOnlyCollection<UserDTO>> GetAsync();
    Task<Status> DeleteAsync(int UserId);
}