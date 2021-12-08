namespace SE_training.Core;

public interface ITagRepository
{
    Task<(Status status, TagDTO tag)> CreateAsync(CreateTagDTO tag);
    Task<IReadOnlyCollection<TagDTO>> ReadAsync(int materialId);
    Task<IReadOnlyCollection<TagDTO>> ReadAsync();
    Task<Status> DeleteAsync(int tagId);
}