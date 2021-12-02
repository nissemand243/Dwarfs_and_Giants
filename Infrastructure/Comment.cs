namespace SE_Training.Infrastructure;
public class Comment
{
    
    public int UserId { get; set; } 

    [StringLength(500)]
    [Required]
    public string? UserComment { get; set; }
}