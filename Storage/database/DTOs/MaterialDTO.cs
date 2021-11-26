namespace DTOs;

public record MaterialDTO
{
    public int materialID { get; init; }
    public int userID { get; init; }
    public string title { get; init; }
    public string description { get; init; }

    public string fileType { get; init; }
    public string filePath { get; init; }
}