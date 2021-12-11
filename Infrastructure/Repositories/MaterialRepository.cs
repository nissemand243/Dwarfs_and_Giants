using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SE_training.Infrastructure;

public class MaterialRepository : IMaterialRepository
{
    private readonly IDatabaseContext _context;

    public MaterialRepository(IDatabaseContext context)
    {
      _context = context;
    }


    public Task<(Status, MaterialDTO)> CreateMaterial(CreateMaterialDTO material)
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateMaterial(int materialId, MaterialDTO material)
    {
        throw new NotImplementedException();
    }

    public async Task<MaterialDTO> ReadAsync(int MaterialId)
    {
        var materials = from m in _context.Materials
            where m.Id == MaterialId
            select new MaterialDTO(m.Id, m.AuthorId, m.Name, m.Description, m.FileType.ToString(), m.FilePath);
        return await materials.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<MaterialDTO>> ReadAllAsync()
    {
        var materials = from m in _context.Materials
            select new MaterialDTO(m.Id, m.AuthorId, m.Name, m.Description, m.FileType.ToString(), m.FilePath);
        var materialsList = await materials.ToListAsync().ConfigureAwait(false);
        return materialsList.AsReadOnly();
    }

    public async Task<Status> DeleteAsync(int MaterialId)
    {
        Material material = _context.Materials.Find(MaterialId);
        if(material is null) return Status.NotFound;
        
        _context.Materials.Remove(material);

        _context.SaveChanges();

        return Status.Deleted;
    }

    

}
