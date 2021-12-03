namespace SE_training.DTOs;

public record MaterialDTO(int materialId, int userId, string title, string description, string fileType, string filePath);
public record CreateMaterialDTO(int userId, string title, string description, string fileType, string filePath);