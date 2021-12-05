namespace SE_training.Core;

public record CreateCommentDTO(int MaterialId, int UserId, string Text);
public record CommentDTO(int CommentId, int MaterialId, int UserId, string Text) : CreateCommentDTO(MaterialId, UserId, Text);