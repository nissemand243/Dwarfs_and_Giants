namespace SE_training.DTOs;

public record UserDTO(int userId, string userName, string email, string type);
public record CreateUserDTO(string userName, string email, string type);