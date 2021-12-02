namespace SE_training.DTOs;

public record TagDTO(int tagId, int materialId, string tagName);
public record CreateTagDTO(int materialId, string tagName);