namespace SE_training.Infrastructure;

public class TagRepository : ITagRepository
{
    private readonly DatabaseContext _context;

    public TagRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, TagDTO tag)> PutAsync(CreateTagDTO tag)
    {
        var entity = new Tag(tag.TagName)
        {
            MaterialId = tag.MaterialId,
        };
        _context.Tags.Add(entity);
        await _context.SaveChangesAsync();

        var details = new TagDTO(entity.Id, entity.MaterialId, entity.TagName);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<TagDTO>> GetAsync(int materialId)
    {
        var tags = from t in _context.Tags
                   where t.MaterialId == materialId
                   select new TagDTO(t.Id, t.MaterialId, t.TagName);

        return await tags.ToListAsync();
    }

    public async Task<IReadOnlyCollection<TagDTO>> GetAsync()
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