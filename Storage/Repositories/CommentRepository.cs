namespace SE_training.Repositories;

public class CommentRepository : ICommentRepository
{
    readonly DatabaseContext context;

    public CommentRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status, CommentDTO)> PutAsync(CreateCommentDTO comment)
    {
        var entity = new Comment
        {
            MaterialId = comment.MaterialId,
            UserId = comment.UserId,
            Text = comment.Text
        };
        context.Comments.Add(entity);
        await context.SaveChangesAsync();

        var details = new CommentDTO(entity.CommentId, entity.MaterialId, entity.UserId, entity.Text);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<CommentDTO>> GetAsync(int MaterialId)
    {
        var comments = from c in context.Comments
                       where c.MaterialId == MaterialId
                       select new CommentDTO(c.CommentId, c.MaterialId, c.UserId, c.Text);

        return await comments.ToListAsync();
    }

    public async Task<IReadOnlyCollection<CommentDTO>> GetAsync()
    {
        return (await context.Comments
                             .Select(c => new CommentDTO(c.CommentId, c.MaterialId, c.UserId, c.Text))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> DeleteAsync(int CommentId)
    {
        var entity = await context.Comments.FindAsync(CommentId);

        if (entity == null) return NotFound;

        context.Comments.Remove(entity);
        await context.SaveChangesAsync();

        return Deleted;
    }
}