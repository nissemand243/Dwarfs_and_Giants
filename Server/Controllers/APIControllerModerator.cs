namespace SE_training.Server.Controllers;

public class APIControllerModerator : APIControllerBase, IAPIControllerModerator
{
    public Task<Status> DeleteMaterial(int materialId)
    {
        throw new NotImplementedException();
    }

    public Task<(Status, MaterialDTO)> PostMaterial(int materialId, MaterialCreateDTO material)
    {
        throw new NotImplementedException();
    }

    public Task<Status> PutMaterial(int materialId, MaterialUpdateDTO material)
    {
        throw new NotImplementedException();
    }
}
