namespace Core;

public interface IAPIControllerModerator
{
    Task<Status> DeleteMaterial(int materialId);
    Task<Status> PutMaterial(int materialId, MaterialUpdateDTO material);
    Task<(Status, MaterialDTO)> PostMaterial(int materialId, MaterialCreateDTO material); 
}
