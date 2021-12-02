namespace SE_training.DTOs;

public record CommentDTO(int commentId, int materialId, int userId, string text);
public record CreateCommentDTO(int materialId, int userId, string text);