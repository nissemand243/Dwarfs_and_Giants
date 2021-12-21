namespace SE_training.Core;

public interface ICommentRepository
{
    Task<(Status status, CommentDTO comment)> CreateAsync(CreateCommentDTO comment);
    Task<(Status status, IReadOnlyCollection<CommentDTO> comments)> ReadAsync(int materialId);
    Task<Status> DeleteAsync(int commentId);

    Task<Status> DeleteAllAsync(int materialId);
}