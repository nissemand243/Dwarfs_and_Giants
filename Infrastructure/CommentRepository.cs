namespace SE_Training.Infrastructure;
public class CommentRepository
{
    private readonly ISETrainingContext _context;

    public CommentRepository(ISETrainingContext context)
    {
        _context = context;
    }

    
}
