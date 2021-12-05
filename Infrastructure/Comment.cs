namespace SE_training.Infrastructure;
public class Comment
{
    public int CommentId { get; set; }

    public int UserId { get; set; }
    public int MaterialId { get; set; }

    [StringLength(500)]
    [Required]
    public string? Text { get; set; }
}