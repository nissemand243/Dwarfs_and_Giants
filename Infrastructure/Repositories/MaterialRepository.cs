namespace SE_training.Infrastructure;

public class MaterialRepository : IMaterialRepository
{
    private readonly IDatabaseContext _context;

    public MaterialRepository(IDatabaseContext context)
    {
      _context = context;
    }

    public async Task<(Status, MaterialDTO)> CreateMaterialAsync(CreateMaterialDTO material)
    {
        var newMaterial = new Material
        {
            AuthorId = material.AuthorId,
            Description = material.Description,
            FilePath = material.FilePath,
            FileType = material.FileType,
            Name = material.Name
        };
        _context.Materials.Add(newMaterial);

        await _context.SaveChangesAsync();

        return (Status.Created, new MaterialDTO(
            newMaterial.Id, 
            newMaterial.AuthorId, 
            newMaterial.Name, 
            newMaterial.Description, 
            newMaterial.FileType, 
            newMaterial.FilePath));
    }

    public async Task<Status> UpdateMaterialAsync(int materialId, CreateMaterialDTO material)
    {
        var entity = await _context.Materials.FirstOrDefaultAsync(m => m.Id == materialId);
        if (entity is null) return NotFound;

        entity.AuthorId = material.AuthorId;
        entity.Name = material.Name;
        entity.Description = material.Description;
        entity.FileType =  material.FileType;
        entity.FilePath = material.FilePath;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    public async Task<MaterialDTO> ReadAsync(int MaterialId)
    {
        var materials = from m in _context.Materials
            where m.Id == MaterialId
            select new MaterialDTO(m.Id, m.AuthorId, m.Name, m.Description, m.FileType, m.FilePath);
        return await materials.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<MaterialDTO>> ReadAllAsync()
    {
        var materials = from m in _context.Materials
            select new MaterialDTO(m.Id, m.AuthorId, m.Name, m.Description, m.FileType, m.FilePath);
        var materialsList = await materials.ToListAsync();

        return materialsList.AsReadOnly();
    }

    public async Task<Status> DeleteAsync(int MaterialId)
    {
        var material = _context.Materials.FindAsync(MaterialId);
        if(material.Result is null) return Status.NotFound;
        
        _context.Materials.Remove(material.Result);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }
}
