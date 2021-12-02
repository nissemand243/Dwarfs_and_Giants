namespace DTOs;

public record TagDTO(int tagId, int materialId, string category);
public record CreateTagDTO(int materialId, string category);