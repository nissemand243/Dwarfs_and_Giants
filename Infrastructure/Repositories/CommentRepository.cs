namespace SE_training.Infrastructure;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _context;

    public CommentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, CommentDTO comment)> PutAsync(CreateCommentDTO comment)
    {
        var entity = new Comment(comment.Text)
        {
            MaterialId = comment.MaterialId,
            UserId = comment.UserId,
        };
        _context.Comments.Add(entity);
        await _context.SaveChangesAsync();

        var details = new CommentDTO(entity.Id, entity.MaterialId, entity.UserId, entity.Text);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<CommentDTO>> GetAsync(int materialId)
    {
        var comments = from c in _context.Comments
                       where c.MaterialId == materialId
                       select new CommentDTO(c.Id, c.MaterialId, c.UserId, c.Text);

        return await comments.ToListAsync();
    }

    public async Task<IReadOnlyCollection<CommentDTO>> GetAsync()
    {
        return (await _context.Comments
                             .Select(c => new CommentDTO(c.Id, c.MaterialId, c.UserId, c.Text))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> DeleteAsync(int commentId)
    {
        var entity = await _context.Comments.FindAsync(commentId);

        if (entity == null) return NotFound;

        _context.Comments.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }
}