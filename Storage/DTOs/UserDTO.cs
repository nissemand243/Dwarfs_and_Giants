namespace SE_training.DTOs;

public record UserDTO(int userId, string name, string email, string type);
public record CreateUserDTO(string name, string email, string type);