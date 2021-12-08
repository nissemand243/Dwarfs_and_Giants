namespace SE_training.Core;

public interface IMaterialRepository
{
    Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO material);
    Task<MaterialDTO> ReadAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> ReadAsync();
    Task<Status> DeleteAsync(int MaterialId);
}