namespace SE_training.Infrastructure;
public class Rating
{
    private Rating() { }
    public Rating(int materialId, int userId, int rating)
    {
        MaterialId = materialId;
        UserId = userId;
        Value = rating;
    }

    public int Id { get; set; }
    public int MaterialId { get; set; }
    public int UserId { get; set; }
    [Range(1, 6)]
    public int Value { get; set;}

}