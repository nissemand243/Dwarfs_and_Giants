namespace DTOs;

public record RatingDTO
{
    public int materialID { get; init; }
    public int userID { get; init; }
    public int value { get; init; }
}