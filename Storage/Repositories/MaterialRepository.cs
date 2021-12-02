namespace SE_training.Repositories;

public class MaterialRepository
{
    readonly DatabaseContext context;

    public MaterialRepository(DatabaseContext _context)
    {
        context = _context;
    }

    /*public async void Put(MaterialDTO material)
    {
        context.Materials.Add(material);
        await context.SaveChangesAsync();
    }

    public IEnumerable<MaterialDTO> GetByID(int materialID)
    {
        return from m in context.Materials
               where m.materialID == materialID
               select new MaterialDTO(m.materialID, m.userID, m.title, m.description, m.fileType, m.filePath);
    }

    public IEnumerable<MaterialDTO> GetByTitle(string title)
    {
        return from m in context.Materials
               where m.title.Contains(title)
               select new MaterialDTO(m.materialID, m.userID, m.title, m.description, m.fileType, m.filePath);
    }

    public async void Delete(int materialID)
    {
        var material = await context.Materials.FindAsync(materialID);
        if (material != null)
        {
            context.Materials.Remove(material);
        }
        await context.SaveChangesAsync();
    }*/
}