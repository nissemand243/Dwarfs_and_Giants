namespace Infrastructure;
public class RatingRepository
{
    private readonly ISETrainingContext _context;

    public RatingRepository(ISETrainingContext context)
    {
        _context = context;
    }
}
