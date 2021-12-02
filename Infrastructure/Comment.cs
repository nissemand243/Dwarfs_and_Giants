namespace SE_training.Core;

public class Comment
{
    
    public int UserId { get; set; } 

    [StringLength(500)]
    [Required]
    public string UserComment { get; set; }
}