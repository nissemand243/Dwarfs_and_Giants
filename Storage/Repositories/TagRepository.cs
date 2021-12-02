namespace SE_training.Repositories;

public class TagRepository : ITagRepository
{
    readonly DatabaseContext context;

    public TagRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status, TagDTO)> PutAsync(CreateTagDTO tag)
    {
        var entity = new Tag
        {
            MaterialId = tag.MaterialId,
            TagName = tag.TagName
        };
        context.Tags.Add(entity);
        await context.SaveChangesAsync();

        var details = new TagDTO(entity.TagId, entity.MaterialId, entity.TagName);
        return (Created, details);
    }

    public async Task<IReadOnlyCollection<TagDTO>> GetAsync(int MaterialId)
    {
        var tags = from t in context.Tags
                   where t.MaterialId == MaterialId
                   select new TagDTO(t.TagId, t.MaterialId, t.TagName);

        return await tags.ToListAsync();
    }

    public async Task<IReadOnlyCollection<TagDTO>> GetAsync()
    {
        return (await context.Tags
                             .Select(t => new TagDTO(t.TagId, t.MaterialId, t.TagName))
                             .ToListAsync()).AsReadOnly();
    }

    public async Task<Status> DeleteAsync(int TagId)
    {
        var entity = await context.Tags.FindAsync(TagId);

        if (entity == null) return NotFound;

        context.Tags.Remove(entity);
        await context.SaveChangesAsync();

        return Deleted;
    }
}