namespace SE_training.Core;

public interface ITagRepository
{
    Task<(Status status, TagDTO tag)> PutAsync(CreateTagDTO tag);
    Task<IReadOnlyCollection<TagDTO>> GetAsync(int materialId);
    Task<IReadOnlyCollection<TagDTO>> GetAsync();
    Task<Status> DeleteAsync(int tagId);
}