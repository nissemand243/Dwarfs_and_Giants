namespace Infrastructure;
public class MaterialRepository
{
    private readonly ISETrainingContext _context;

    public MaterialRepository(ISETrainingContext context)
    {
        _context = context;
    }
}
