namespace SE_training.DTOs;

public record RatingDTO(int ratingId, int materialdId, int userId, int value);
public record CreateRatingDTO(int materialId, int userId, int value);