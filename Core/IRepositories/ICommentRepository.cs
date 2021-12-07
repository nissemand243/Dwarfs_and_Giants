namespace SE_training.Core;

public interface ICommentRepository
{
    Task<(Status status, CommentDTO comment)> PutAsync(CreateCommentDTO comment);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync(int materialId);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync();
    Task<Status> DeleteAsync(int commentId);
}