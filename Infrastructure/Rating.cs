namespace SE_training.Infrastructure;

public class Rating
{
    public int id {get; set; }

    [StringLength(50)]
    public int rating {get; set; }
}