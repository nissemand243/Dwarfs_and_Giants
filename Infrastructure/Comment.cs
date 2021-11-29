namespace SE_training.Infrastructure;

public class Comment
{
    public int id {get; set; }
    [Required]
    public string? comment {get; set; }

}