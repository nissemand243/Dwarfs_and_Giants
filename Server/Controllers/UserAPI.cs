namespace SE_training.Server.Controllers;

public class UserAPI
{
    private CommentController _commentController;
    private RatingController _ratingController;
    private MaterialController _materialController;
    
    public Task<Response> DeleteMaterial(int materialId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> PatchComment(int id, CommentDTO comment)
    {
        throw new NotImplementedException();
    }
    public Task<Response> PatchRating(int id, RatingDTO rating)
    {
        throw new NotImplementedException();
    }
    public Task<Response> PutMaterial(int materialId, MaterialUpdateDTO material)
    {
        throw new NotImplementedException();
    }
    public Task<(Response, MaterialDTO)> Get(int id)
    {
        throw new NotImplementedException();
    }
    public Task<(Response, MaterialDTO)> PostMaterial(int materialId, MaterialCreateDTO material)
    {
        throw new NotImplementedException();
    }
    public Task<(Response, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput)
    {
        throw new NotImplementedException();
    }
   
}
