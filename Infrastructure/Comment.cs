namespace SE_training.Infrastructure;
public class Comment
{
    private Comment() { }
    public Comment(string text)
    {
        Text = text;
    }
    public int Id { get; set; }

    public int UserId { get; set; }
    public int MaterialId { get; set; }

    [StringLength(500)]
    [Required]
    public string Text { get; set; }
    

    
}