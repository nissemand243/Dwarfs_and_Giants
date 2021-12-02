namespace DTOs;

public record RatingDTO(int ratingId, int materialdId, int userId, int value);
public record CreateRatingDTO(int materialdId, int userId, int value);