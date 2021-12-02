namespace SE_training.IRepositories;

public interface ITagRepository
{
    Task<(Status, TagDTO)> PutAsync(CreateTagDTO tag);
    Task<IReadOnlyCollection<TagDTO>> GetAsync(int MaterialId);
    Task<IReadOnlyCollection<TagDTO>> GetAsync();
    Task<Status> DeleteAsync(int TagId);
}