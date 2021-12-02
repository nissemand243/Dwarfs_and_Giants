namespace SE_training.Core;

public class CommentRepository
{
    private readonly ISETrainingContext _context;

    public CommentRepository(ISETrainingContext context)
    {
        _context = context;
    }

    
}
