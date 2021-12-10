namespace SE_training.Core;

public record CommentDTO(int Id, int MaterialId, int UserId, string? Text) : CreateCommentDTO(MaterialId, UserId, Text);
public record CreateCommentDTO(int MaterialId, int UserId, string? Text);
