namespace SE_training.Infrastructure;

public class MaterialRepository : IMaterialRepository
{
    private readonly DatabaseContext _context;

    public MaterialRepository(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<MaterialDTO> ReadAsync(int searchId)
    {
        var materials = from c in _context.Materials
                        where c.Id == searchId
                        select new MaterialDTO(
                            c.Id,
                            c.AuthorId,
                            c.Name,
                            c.Description,
                            c.FileType.ToString(),
                            c.FilePath
                        );
        return await materials.FirstOrDefaultAsync();
    }


    //Delete material
    public async Task<Status> DeleteAsync(int materialId)
    {

        //Find material by the material ID asynchronically as entity
        var entity = await _context.Materials.FindAsync(materialId);


        //If there is no entity with the given id return not found
        if (entity == null)
        {
            return NotFound;
        }


        //Remove the found entity from the context and save changes
        _context.Materials.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }

   public Task<(Status, MaterialDTO)> CreateMaterial(CreateMaterialDTO material)
    {
        throw new NotImplementedException();
    }

    public Task<MaterialDTO> GetAsync(int MaterialId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<MaterialDTO>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateMaterial(int materialId, MaterialDTO material)
    {
        throw new NotImplementedException();
    }

}