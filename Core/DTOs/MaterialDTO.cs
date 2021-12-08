namespace SE_training.Core;

public record CreateMaterialDTO(int AuthorId, string Name, string? Description, string FileType, string FilePath);
public record MaterialDTO(int Id, int AuthorId, string Name, string? Description, string FileType, string FilePath) : CreateMaterialDTO(AuthorId, Name, Description, FileType, FilePath);
public record DetailsMaterialDTO
(
    [Required]
    int Id,

    [Required]
    int AuthorId,

    [Required]
    string Name,

    string? Description,

    [Required]
    string FileType,

    [Required]
    string FilePath,

    ICollection<TagDTO> Tags,

    ICollection<CommentDTO> Comments,

    double Rating
    ) : MaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath);