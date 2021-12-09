namespace SE_training.Infrastructure;
public class Rating
{
    public int Id { get; set; }

    public int MaterialId { get; init; }

    public int UserId { get; init; }

    [Range(1, 6)]
    public int Value { get; set; }
}