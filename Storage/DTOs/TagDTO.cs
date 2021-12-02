namespace SE_training.DTOs;

public record CreateTagDTO(int MaterialId, string TagName);
public record TagDTO(int TagId, int MaterialId, string TagName) : CreateTagDTO(MaterialId, TagName);