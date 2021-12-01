namespace SE_training.Core
{
    public record CommentDto([Required] int UserId, [Required, StringLength(500)] string UserComment);

}
