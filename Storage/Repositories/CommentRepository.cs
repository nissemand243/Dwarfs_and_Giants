namespace Repositories;

public class CommentRepository
{
    readonly DatabaseContext context;

    public CommentRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async void Put(CommentDTO comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
    }

    public IEnumerable<CommentDTO> Get(int materialID)
    {
        return from c in context.Comments
               where c.materialID == materialID
               select new CommentDTO(c.materialID, c.userID, c.text);
    }

    public async void Delete(int materialID, int userID, string text)
    {
        var comment = await context.Comments.FindAsync(materialID, userID, text);
        if (comment != null)
        {
            context.Comments.Remove(comment);
        }
        await context.SaveChangesAsync();
    }
}