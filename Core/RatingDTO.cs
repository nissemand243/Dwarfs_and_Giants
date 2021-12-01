namespace SE_training.Core

{
    public record RatingDto([Required]int UserId, [Required, Range(1,6)] int UserRating);
}
