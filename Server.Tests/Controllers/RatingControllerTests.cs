namespace SE_training.Server.Tests.Controllers;

public class RatingControllerTests
{
    [Fact]
    public async Task CreateRating_creates_rating_returns_rating_and_status_Created()
    {
        // Arrange
        var createRatingDto = new CreateRatingDTO(1,1,5);
        var ratingDTO = new RatingDTO(1,1,1,5);
        var Expected = (Status.Created, ratingDTO);

        var repository = new Mock<IRatingRepository>();
        repository.Setup(repo => repo.CreateAsync(createRatingDto)).ReturnsAsync(Expected);
        var logger = new Mock<ILogger<RatingController>>();
        var controller = new RatingController(logger.Object, repository.Object);

        // Act
        var result = await controller.CreateRating(createRatingDto);

        // Assert
        Assert.Equal(Expected.ratingDTO, result.rating);
        Assert.Equal(Expected.Created, result.status);       
    }

    [Fact]
    public async Task DeleteRating_given_existing_returns_status_Deleted()
    {
        // Arrange
        var expected = Status.Deleted;

        var repository = new Mock<IRatingRepository>();
        repository.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<RatingController>>();
        var controller = new RatingController(logger.Object, repository.Object);
    
        // Act
        var actual = await controller.DeleteRating(1);
    
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
     public async Task DeleteRating_given_non_existing_returns_status_NotFound()
    {
        // Arrange
        var expected = Status.NotFound;

        var repository = new Mock<IRatingRepository>();
        repository.Setup(repo => repo.DeleteAsync(999)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<RatingController>>();
        var controller = new RatingController(logger.Object, repository.Object);

        // Act
        var actual = await controller.DeleteRating(999);
    
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAllRatings_given_existing_materialid_returns_ratingDTOs()
    {
        // Arrange
        var ratingOne = new RatingDTO(1,2,0,3);
        var ratingTwo = new RatingDTO(2,2,1,5);
        var expected = new List<RatingDTO> {ratingOne,ratingTwo}.AsReadOnly();

        var repository = new Mock<IRatingRepository>();
        repository.Setup(repo => repo.ReadAsync(2)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<RatingController>>();
        var controller = new RatingController(logger.Object, repository.Object);
    
        // Act

        var actual = await controller.ReadAllRatings(2);
    
        // Assert
        Assert.Collection(actual,
            actualDTO => Assert.Equal(expected[0], actualDTO),
            actualDTO => Assert.Equal(expected[1], actualDTO)
        );
    }

    [Fact]
    public async Task DeleteAllRatings_given_existing_materialid_returns_status_Deleted()
    {
        // Arrange
        var expected = Status.Deleted;

        var repository = new Mock<IRatingRepository>();
        repository.Setup(repo => repo.DeleteAllAsync(1)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<RatingController>>();
        var controller = new RatingController(logger.Object, repository.Object);
    
        // Act
        var actual = await controller.DeleteAllRatings(1);
    
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteAllRatings_given_existing_materialid_with_no_ratings_returns_status_NotFound()
    {
        // Arrange
        var expected = Status.NotFound;
        var repository = new Mock<IRatingRepository>();
        repository.Setup(repo => repo.DeleteAllAsync(42)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<RatingController>>();
        var controller = new RatingController(logger.Object, repository.Object);
    
        // Act
        var actual = await controller.DeleteAllRatings(42);
    
        // Assert
        Assert.Equal(expected, actual);
    }
}