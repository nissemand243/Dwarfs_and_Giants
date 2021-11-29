namespace SE_training.Server.Controllers;

public class APIControllerModerator : APIControllerBase, IAPIControllerModerator
{
    public Task<Response> DeleteMaterial(int materialId)
    {
        throw new NotImplementedException();
    }

    public Task<(Response, MaterialDTO)> PostMaterial(int materialId, MaterialCreateDTO material)
    {
        throw new NotImplementedException();
    }

    public Task<Response> PutMaterial(int materialId, MaterialUpdateDTO material)
    {
        throw new NotImplementedException();
    }
}
