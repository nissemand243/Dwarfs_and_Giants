namespace SE_training.Core;

public interface IStudentController
{
    Task<(Status status, CommentDTO comment)> PatchComment(CreateCommentDTO comment);
    Task<(Status status, RatingDTO rating)> PatchRating(CreateRatingDTO rating);
    Task<(Status, DetailsMaterialDTO)> Get(int id);
    Task<IReadOnlyCollection<MaterialDTO>> Search(string searchInput);
}
