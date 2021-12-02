namespace SE_training.DTOs;

public record CreateMaterialDTO(int UserId, string Title, string Description, string FileType, string FilePath);
public record MaterialDTO(int MaterialId, int UserId, string Title, string Description, string FileType, string FilePath) : CreateMaterialDTO(UserId, Title, Description, FileType, FilePath);
/*public record CreateMaterialDTO
{
    [Required]
    public int UserId { get; init; }

    [Required]
    public string Title { get; init; }

    public string Description { get; init; }

    public string FileType { get; init; }

    [Required]
    public string FilePath { get; init; }
}*/