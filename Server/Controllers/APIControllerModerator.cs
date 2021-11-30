namespace SE_training.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class APIControllerModerator : APIControllerBase, IAPIControllerModerator
{
    private readonly ILogger<APIControllerModerator> _logger;
    public APIControllerModerator(ILogger<APIControllerModerator> logger) : base (logger)
    {
        _logger = logger;
    }

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
