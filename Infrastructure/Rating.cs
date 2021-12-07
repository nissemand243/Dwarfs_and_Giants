namespace SE_training.Infrastructure;
public class Rating
{
    public int Id { get; init; }
    public int MaterialId { get; }
    public int UserId { get; }
    [Range(1, 6)]
    public int Value { get; set;}

    public Rating(int materialId, int userId, int rating)
    {
        MaterialId = materialId;
        UserId = UserId;
        Value = rating;
    }
}