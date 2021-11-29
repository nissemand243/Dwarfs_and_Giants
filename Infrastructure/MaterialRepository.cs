namespace SE_training.Infrastructure;

public class MaterialRepository : IMaterialRepository
{
    public Task<(Status, MaterialDTO)> CreateAsync(MaterialCreateDTO Material)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int MaterialId)
    {
        throw new NotImplementedException();
    }

    public Task<MaterialDTO> ReadAsync(int MaterialId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<MaterialDTO>> ReadAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(MaterialDTO Material)
    {
        throw new NotImplementedException();
    }
}