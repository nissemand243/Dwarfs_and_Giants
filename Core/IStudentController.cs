namespace SE_training.Core;

public interface IStudentController
{
    Task<(Status status, CommentDTO comment)> PatchComment(CreateCommentDTO comment);
    Task<Status> PatchRating(int id, RatingDTO rating);
    Task<(Status, DetailsMaterialDTO)> Get(int id);
    Task<(Status, IReadOnlyCollection<MaterialDTO>)> Search(string searchInput);
}
