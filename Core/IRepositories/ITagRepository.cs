namespace SE_training.Core;

public interface ITagRepository
{
    Task<(Status, TagDTO)> PutAsync(CreateTagDTO tag);
    Task<IReadOnlyCollection<TagDTO>> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<TagDTO>> GetAsync();
    Task<Status> DeleteAsync(int TagId);
}