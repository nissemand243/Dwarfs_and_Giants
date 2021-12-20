namespace SE_training.Core;

public record CreateUserDTO(string? Name, string? Email);
public record UserDTO(int Id, string? Name, string Email) : CreateUserDTO(Name, Email);
