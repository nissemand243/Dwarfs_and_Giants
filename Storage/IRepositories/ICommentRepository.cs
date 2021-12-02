namespace SE_training.IRepositories;

public interface ICommentRepository
{
    Task<(Status, CommentDTO)> PutAsync(CreateCommentDTO comment);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync(int materialId);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync();
    Task<Status> DeleteAsync(int commentId);
}