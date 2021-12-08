namespace SE_training.Infrastructure;

public class TagRepository : ITagRepository
{
    private readonly IDatabaseContext _context;

    public TagRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, TagDTO tag)> CreateAsync(CreateTagDTO tag)
    {
        var entity = new Tag()
        {
            MaterialId = tag.MaterialId,
            TagName = tag.TagName
        };
        _context.Tags.Add(entity);
        await _context.SaveChangesAsync();

        var details = new TagDTO(entity.Id, entity.MaterialId, entity.TagName);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<TagDTO>> ReadAsync(int materialId)
    {
        var tags = from t in _context.Tags
                   where t.MaterialId == materialId
                   select new TagDTO(t.Id, t.MaterialId, t.TagName);

        return await tags.ToListAsync();
    }

    public async Task<IReadOnlyCollection<TagDTO>> ReadAsync()
    {
        return (await _context.Tags
                             .Select(t => new TagDTO(t.Id, t.MaterialId, t.TagName))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> DeleteAsync(int tagId)
    {
        var entity = await _context.Tags.FindAsync(tagId);

        if (entity == null) return NotFound;

        _context.Tags.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }
}