namespace SE_training.IRepositories;

public interface ICommentRepository
{
    Task<(Status, CommentDTO)> PutAsync(CreateCommentDTO comment);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<CommentDTO>> GetAsync();
    Task<Status> DeleteAsync(int CommentId);
}