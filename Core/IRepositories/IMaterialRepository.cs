namespace SE_training.Core;

public interface IMaterialRepository
{
    Task<(Status, MaterialDTO)> PutAsync(CreateMaterialDTO material);
    Task<MaterialDTO> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> GetAsync();
    Task<Status> DeleteAsync(int MaterialId);
}