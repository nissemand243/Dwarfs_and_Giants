namespace DTOs;

public record CommentDTO
{
    public int materialID { get; init; }
    public int userID { get; init; }
    public string text { get; init; }
}