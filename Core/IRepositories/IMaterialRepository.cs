namespace SE_training.Core;

public interface IMaterialRepository
{
    Task<(Status, MaterialDTO)> CreateMaterial(CreateMaterialDTO material);
    Task<Status> UpdateMaterial(int materialId, MaterialDTO material);
    Task<MaterialDTO> ReadAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> ReadAllAsync();
    Task<Status> DeleteAsync(int MaterialId);
}