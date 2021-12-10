namespace SE_training.Core;


public interface IAPIControllerModerator
{
    Task<Status> DeleteMaterial(int MaterialId);
    Task<Status> PutMaterial(int MaterialId, MaterialDTO material);
    Task<(Status status, MaterialDTO material)> PostMaterial(CreateMaterialDTO material);
}

