namespace SE_training.Core;

public record CreateMaterialDTO(int UserId, string Name, string Description, string FileType, string FilePath);
public record MaterialDTO(int MaterialId, int UserId, string Name, string Description, string FileType, string FilePath) : CreateMaterialDTO(UserId, Name, Description, FileType, FilePath);
public record DetailsMaterialDTO
(
    [Required]
    int MaterialId,

    [Required]
    int UserId,

    [Required]
    string Name,

    string Description,

    [Required]
    string FileType,

    [Required]
    string FilePath,

    ICollection<TagDTO> Tags,

    ICollection<CommentDTO> Comments,

    IDictionary<string, int> Ratings
    ) : MaterialDTO(MaterialId, UserId, Name, Description, FileType, FilePath);