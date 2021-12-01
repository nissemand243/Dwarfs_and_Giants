namespace SE_training.Infrastructure;
public class Tag
{   
    [Required]
    [StringLength(25)]
    public string Name { get; set; } 
}