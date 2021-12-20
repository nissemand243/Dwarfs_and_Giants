namespace SE_training.Core;

public record CreateUserDTO(string? Name, string? Email, string Role);
public record UserDTO(int Id, string? Name, string? Email, string Role) : CreateUserDTO(Name, Email, Role);