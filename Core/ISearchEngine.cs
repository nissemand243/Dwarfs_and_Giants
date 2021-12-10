namespace SE_training.Core;

public interface ISEarchEngine
{
    public Task<IList<DetailsMaterialDTO>> SearchAsync(string searchString);
    public Task<IList<DetailsMaterialDTO>> SearchByNameAsync(string searchString);
    public Task<IList<DetailsMaterialDTO>> SearchByDescriptionAsync(string searchString);
    public Task<IList<DetailsMaterialDTO>> SearchByTagsAsync(string searchString);
    public Task<IList<DetailsMaterialDTO>> SearchByAuthorAsync(string searchString);
    public Task<DetailsMaterialDTO> GetDetailedMaterialByIdAsync(int materialId);
    public Task<IList<DetailsMaterialDTO>> GetRelatedMaterialsByTagsAsync(int materialId);
}