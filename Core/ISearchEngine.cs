namespace SE_training.Core;

public interface ISEarchEngine
{
    public Task<IReadOnlyCollection<MaterialDTO>> SearchAsync(string searchString);
    public Task<IList<MaterialDTO>> SearchByNameAsync(string searchString);
    public Task<IList<MaterialDTO>> SearchByDescriptionAsync(string searchString);
    public Task<IList<MaterialDTO>> SearchByTagsAsync(string searchString);
    public Task<IList<MaterialDTO>> SearchByAuthorAsync(string searchString);
    public Task<DetailsMaterialDTO?> GetDetailedMaterialByIdAsync(int materialId);
    public Task<IReadOnlyCollection<MaterialDTO>> GetRelatedMaterialsByTagsAsync(int materialId);
}