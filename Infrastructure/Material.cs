namespace SE_training.Infrastructure;

public class Material
{
    public int Id { get; init; }
    public int AuthorId { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FileType FileType { get; set; }

    [Url]
    public string FilePath { get; set; }

    public Material(int id, int authorId, string name, string description, FileType fileType, string filePath)
    {
        Id = id;
        AuthorId = authorId;
        Name = name;
        Description = description;
        FileType = fileType;
        FilePath = filePath;  
    }
    
}