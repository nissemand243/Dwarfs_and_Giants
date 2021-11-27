namespace SETraining.Infrastructure;

public class CommentRepository : ICommentRepository
{
    public Task<(Status, CommentDTO)> CreateAsync(CommentCreateDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO> ReadAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<CommentDTO>> ReadAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(CommentDTO comment)
    {
        throw new NotImplementedException();
    }
}