namespace SE_training.Infrastructure;
public class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }
    
    public int MaterialId { get; set; }

    [StringLength(500)]
    public string? Text { get; set; }    
}