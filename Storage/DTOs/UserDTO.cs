namespace SE_training.DTOs;

public record CreateUserDTO(string Name, string Email, string Type);
public record UserDTO(int UserId, string Name, string Email, string Type) : CreateUserDTO(Name, Email, Type);