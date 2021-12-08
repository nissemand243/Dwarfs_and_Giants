namespace SE_training.Infrastructure;

public class Material
{
    public int Id { get; set; }

    public int AuthorId { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    public FileType FileType { get; set; }

    [Required]
    [Url]
    public string? FilePath { get; set; }
}