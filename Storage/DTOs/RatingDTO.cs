namespace SE_training.DTOs;

public record CreateRatingDTO(int MaterialId, int UserId, int Value);
public record RatingDTO(int RatingId, int MaterialId, int UserId, int Value) : CreateRatingDTO(MaterialId, UserId, Value);