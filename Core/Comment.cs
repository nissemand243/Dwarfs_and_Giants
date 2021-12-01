namespace SE_training.Core;
public class Comment
{
    [Required]
    public int UserId { get; set; } 

    [Required, StringLength(500)]
    
    public string UserComment { get; set; }
}