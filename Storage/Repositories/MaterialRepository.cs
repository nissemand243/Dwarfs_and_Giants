namespace SE_training.Repositories;

public class MaterialRepository
{
    readonly DatabaseContext context;

    public MaterialRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<MaterialDTO> ReadAsync(int searchId)
    {
        var materials = from c in context.Materials
                        where c.MaterialId == searchId
                        select new MaterialDTO(
                            c.MaterialId,
                            c.UserId,
                            c.Title,
                            c.Description,
                            c.FileType.ToString(),
                            c.FilePath
                        );
        return await materials.FirstOrDefaultAsync();
    }

    /*public async void Put(MaterialDTO material)
    {
        context.Materials.Add(material);
        await context.SaveChangesAsync();
    }

    public IEnumerable<MaterialDTO> GetByID(int MaterialId)
    {
        return from m in context.Materials
               where m.MaterialId == MaterialId
               select new MaterialDTO(m.MaterialId, m.UserId, m.title, m.description, m.fileType, m.filePath);
    }

    public IEnumerable<MaterialDTO> GetByTitle(string title)
    {
        return from m in context.Materials
               where m.title.Contains(title)
               select new MaterialDTO(m.MaterialId, m.UserId, m.title, m.description, m.fileType, m.filePath);
    }

    public async void Delete(int MaterialId)
    {
        var material = await context.Materials.FindAsync(MaterialId);
        if (material != null)
        {
            context.Materials.Remove(material);
        }
        await context.SaveChangesAsync();
    }*/
}