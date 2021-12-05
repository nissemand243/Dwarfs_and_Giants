namespace SE_training.DTOs;

public record CreateMaterialDTO(int UserId, string Title, string Description, string FileType, string FilePath);
public record MaterialDTO(int MaterialId, int UserId, string Title, string Description, string FileType, string FilePath) : CreateMaterialDTO(UserId, Title, Description, FileType, FilePath);
public record DetailsMaterialDTO
(
    [Required]
    int MaterialId,

    [Required]
    int UserId,

    [Required]
    string Title,

    string Description,

    [Required]
    string FileType,

    [Required]
    string FilePath,

    ICollection<TagDTO> Tags,

    ICollection<CommentDTO> Comments,

    IDictionary<string, int> Ratings
    ) : MaterialDTO(MaterialId, UserId, Title, Description, FileType, FilePath);