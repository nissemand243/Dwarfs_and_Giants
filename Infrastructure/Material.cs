namespace SE_training.Infrastructure;

public class Material
{
    private Material() { }
    public Material(int authorId, string name, string description, FileType fileType, string filePath)
    {
        AuthorId = authorId;
        Name = name;
        Description = description;
        FileType = fileType;
        FilePath = filePath;  
    }

    public int Id { get; set; }
    public int AuthorId { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public FileType FileType { get; set; }

    [Url]
    public string FilePath { get; set; }
    
}