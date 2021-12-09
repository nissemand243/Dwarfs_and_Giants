namespace SE_training.Core;

public record CreateUserDTO(string? Name, string? Email, string Type);
public record UserDTO(int Id, string? Name, string? Email, string Type) : CreateUserDTO(Name, Email, Type);