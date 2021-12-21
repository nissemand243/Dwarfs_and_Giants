namespace SE_training.Infrastructure;

public class CommentRepository : ICommentRepository
{
    private readonly IDatabaseContext _context;

    public CommentRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, CommentDTO comment)> CreateAsync(CreateCommentDTO comment)
    {
        var entity = new Comment()
        {
            MaterialId = comment.MaterialId,
            UserId = comment.UserId,
            Text = comment.Text
        };
        _context.Comments.Add(entity);
        await _context.SaveChangesAsync();

        var details = new CommentDTO(entity.Id, entity.MaterialId, entity.UserId, entity.Text);
        return (Status.Created, details);
    }

    public async Task<IReadOnlyCollection<CommentDTO>> ReadAsync(int materialId)
    {
        var comments = await (from c in _context.Comments
                       where c.MaterialId == materialId
                       select new CommentDTO(c.Id, c.MaterialId, c.UserId, c.Text))
                       .ToListAsync();
        return comments;
    }

    public async Task<Status> DeleteAsync(int commentId)
    {
        var entity = await _context.Comments.FindAsync(commentId);

        if (entity == null) return NotFound;

        _context.Comments.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    public async Task<Status> DeleteAllAsync(int materialId)
    {
        var comments = await _context.Comments
            .Where(c => c.MaterialId == materialId)
            .Select(c => c)
            .ToListAsync();

        if (! comments.Any())
        {
            return NotFound; 
        }

        foreach (var comment in comments)
        {
            _context.Comments.Remove(comment);
        }
        await _context.SaveChangesAsync();
        
        return Deleted;
    }
}