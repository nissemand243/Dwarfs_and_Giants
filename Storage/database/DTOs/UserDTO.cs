namespace DTOs;

public record UserDTO
{
    public int userID { get; init; }
    public string userName { get; init; }
    public string email { get; init; }
}