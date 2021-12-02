namespace SE_training.Core;

public interface IAPIControllerModerator
{
    Task<Response> DeleteMaterial(int MaterialId);
    Task<Response> PutMaterial(int MaterialId, MaterialUpdateDTO material);
    Task<(Response, MaterialDTO)> PostMaterial(int MaterialId, MaterialCreateDTO material);
}
