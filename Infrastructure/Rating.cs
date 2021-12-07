namespace SE_training.Infrastructure;
public class Rating
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public int UserId { get; set; }
    [Range(1, 6)]
    public int Value { get; set; }
}