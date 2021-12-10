using System.Collections.Generic;
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


    public async Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO material)
    {
        throw new System.NotImplementedException();
    }

    public async Task<MaterialDTO> ReadAsync(int MaterialId)
    {
        var materials = from m in _context.Materials
            where m.Id == MaterialId
            select new MaterialDTO(m.Id, m.AuthorId, m.Name, m.Description, m.FileType.ToString(), m.FilePath);
        return await materials.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<MaterialDTO>> ReadAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task<Status> DeleteAsync(int MaterialId)
    {
        throw new System.NotImplementedException();
    }
}