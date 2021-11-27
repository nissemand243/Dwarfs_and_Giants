namespace SETraining.Core;

public interface IMaterialRepository
{

    Task<(Status, MaterialDTO)> CreateAsync(MaterialCreateDTO Material);
    Task<MaterialDTO> ReadAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> ReadAsync();
    Task<Status> UpdateAsync(MaterialDTO Material);
    Task<Status> DeleteAsync(int MaterialId);
}