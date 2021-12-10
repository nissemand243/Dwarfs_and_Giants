namespace SE_training.Core;

public interface ISEarchEngine
{
    public Task<ICollection<DetailsMaterialDTO>> SearchMaterialsAsync(string searchString);
    public Task<ICollection<DetailsMaterialDTO>> SearchMaterialsByNameAsync(string searchString);
    public Task<ICollection<DetailsMaterialDTO>> SearchMaterialsByTagsAsync(string searchString);
    public Task<ICollection<DetailsMaterialDTO>> SearchMaterialsByAuthorAsync(string searchString);
    public Task<DetailsMaterialDTO> GetDetailedMaterialByIdAsync(int materialId);
    public Task<ICollection<DetailsMaterialDTO>> GetRelatedMaterialsByTags(int materialId);
}