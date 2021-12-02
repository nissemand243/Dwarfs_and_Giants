namespace SE_training.Repositories;

public class TagRepository
{
    readonly DatabaseContext context;

    public TagRepository(DatabaseContext _context)
    {
        context = _context;
    }

    /*public async void Put(TagDTO tag)
    {
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
    }

    public IEnumerable<TagDTO> Get(int materialID)
    {
        return from t in context.Tags
               where t.materialId == materialID
               select new TagDTO(t.materialId, t.category);
    }

    public async void Delete(int materialID)
    {
        var tag = await context.Tags.FindAsync(materialID);

        while (tag != null)
        {
            context.Tags.Remove(tag);
            tag = await context.Tags.FindAsync(materialID);
        }

        await context.SaveChangesAsync();
    }*/
}