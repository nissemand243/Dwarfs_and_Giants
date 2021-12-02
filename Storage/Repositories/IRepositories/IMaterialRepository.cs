namespace SE_training.IRepositories;

public interface IMaterialRepository
{
    Task<(Status, MaterialDTO)> PutAsync(CreateMaterialDTO material);
    Task<MaterialDTO> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> GetAsync();
    Task<Status> DeleteAsync(int MaterialId);
}