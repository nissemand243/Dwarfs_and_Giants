namespace SE_training.Core;

public interface IMaterialRepository
{
    Task<(Status, MaterialDTO)> CreateMaterial(CreateMaterialDTO material);
    Task<Status> UpdateMaterial(int materialId, MaterialDTO material);
    Task<MaterialDTO> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> GetAsync();
    Task<Status> DeleteAsync(int MaterialId);
}