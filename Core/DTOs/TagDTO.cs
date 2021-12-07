namespace SE_training.Core;

public record CreateTagDTO(int MaterialId, string TagName);
public record TagDTO(int Id, int MaterialId, string TagName) : CreateTagDTO(MaterialId, TagName);