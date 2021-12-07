namespace SE_training.Infrastructure;
public class Comment
{
    public int Id { get; init; }

    public int UserId { get; set; }
    public int MaterialId { get; set; }

    [StringLength(500)]
    [Required]
    public string Text { get; set; }

    public Comment(string text)
    {
        Text = text;
    }
}