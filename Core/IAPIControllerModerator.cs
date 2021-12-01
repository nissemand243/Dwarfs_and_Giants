namespace SE_training.Core;

public interface IAPIControllerModerator
{
    Task<Response> DeleteMaterial(int materialId);
    Task<Response> PutMaterial(int materialId, MaterialUpdateDto material);
    Task<(Response, MaterialDto)> PostMaterial(int materialId, MaterialCreateDto material); 
}
