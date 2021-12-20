namespace SE_training.Core;

public interface ICommentRepository
{
    Task<(Status status, CommentDTO comment)> CreateAsync(CreateCommentDTO comment);
    Task<IReadOnlyCollection<CommentDTO>> ReadAsync(int materialId);
    Task<Status> DeleteAsync(int commentId);

    Task<Status> DeleteAllAsync(int materialId);
}