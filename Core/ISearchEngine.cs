namespace SE_training.Core;

public interface ISEarchEngine
{
    public Task<List<DetailsMaterialDTO>> SearchAsync(string searchString);
    public Task<List<DetailsMaterialDTO>> SearchByNameAsync(string searchString);
    public Task<List<DetailsMaterialDTO>> SearchByDescriptionAsync(string searchString);
    public Task<List<DetailsMaterialDTO>> SearchByTagsAsync(string searchString);
    public Task<List<DetailsMaterialDTO>> SearchByAuthorAsync(string searchString);
    public Task<DetailsMaterialDTO> GetDetailedMaterialByIdAsync(int materialId);
    public Task<List<DetailsMaterialDTO>> GetRelatedMaterialsByTagsAsync(int materialId);
}