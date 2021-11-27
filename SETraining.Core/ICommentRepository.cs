namespace SETraining.Core;

public interface ICommentRepository
{

    Task<(Status, CommentDTO)> CreateAsync(CommentCreateDTO comment);
    Task<CommentDTO> ReadAsync(int commentId);
    Task<IReadOnlyCollection<CommentDTO>> ReadAsync();
    Task<Status> UpdateAsync(CommentDTO comment);
    Task<Status> DeleteAsync(int commentId);
}
