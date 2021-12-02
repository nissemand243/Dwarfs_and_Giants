namespace IRepositories;

public interface IMaterialRepository
{
    Task<(Status, MaterialDTO)> PutAsync(CreateMaterialDTO material);
    Task<MaterialDTO> GetAsync(int materialId);
    Task<IReadOnlyCollection<MaterialDTO>> GetAsync();
    Task<Status> DeleteAsync(int materialId);
}