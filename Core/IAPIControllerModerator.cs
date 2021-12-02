namespace SE_training.Core;

public interface IAPIControllerModerator
{
    Task<Response> DeleteMaterial(int materialId);
    Task<Response> PutMaterial(int materialId, MaterialUpdateDTO material);
    Task<(Response, MaterialDTO)> PostMaterial(int materialId, MaterialCreateDTO material); 
}
