

namespace SE_training.Server.Tests.Controllers;

public class CommentControllerTests
{
    [Fact]
    public async Task CreateComment_creates_comment_returns_comment_and_status_Created()
    {
        // Arrange
        var createCommentDto = new CreateCommentDTO(1,1,"Lorem Ipsum");
        var commentDTO = new CommentDTO(1,1,1,"Lorem Ipsum");
        var Expected = (Status.Created, commentDTO);

        var repository = new Mock<ICommentRepository>();
        repository.Setup(repo => repo.CreateAsync(createCommentDto)).ReturnsAsync(Expected);
        var logger = new Mock<ILogger<CommentController>>();
        var controller = new CommentController(logger.Object, repository.Object);

        // Act
        var result = await controller.CreateComment(createCommentDto);

        // Assert
        Assert.Equal(Expected.commentDTO, result.comment);
        Assert.Equal(Expected.Created, result.status);       
    }

    [Fact]
    public async Task DeleteComment_given_existing_returns_status_Deleted()
    {
        // Arrange
        var expected = Status.Deleted;

        var repository = new Mock<ICommentRepository>();
        repository.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<CommentController>>();
        var controller = new CommentController(logger.Object, repository.Object);
    
        // Act
        var actual = await controller.DeleteComment(1);
    
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
     public async Task DeleteComment_given_non_existing_returns_status_NotFound()
    {
        // Arrange
        var expected = Status.NotFound;

        var repository = new Mock<ICommentRepository>();
        repository.Setup(repo => repo.DeleteAsync(21)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<CommentController>>();
        var controller = new CommentController(logger.Object, repository.Object);

        // Act
        var actual = await controller.DeleteComment(21);
    
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAllComments_given_existing_materialid_returns_commentDTOs()
    {
        // Arrange
        var commentOne = new CommentDTO(1,2,0, "Lorem ipsum comment");
        var commentTwo = new CommentDTO(2,2,1, "Lorem ipsum comment, by master ipsum");
        var expected = new List<CommentDTO> {commentOne,commentTwo}.AsReadOnly();

        var repository = new Mock<ICommentRepository>();
        repository.Setup(repo => repo.ReadAsync(2)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<CommentController>>();
        var controller = new CommentController(logger.Object, repository.Object);
    
        // Act

        var actual = await controller.ReadAllComments(2);
    
        // Assert
        Assert.Collection(actual,
            aDTO => Assert.Equal(expected[0], aDTO),
            aDTO => Assert.Equal(expected[1], aDTO)
        );
    }

    [Fact]
    public async Task DeleteAllComments_given_existing_materialid_with_comments_returns_status_deleted()
    {
        // Arrange
        var expected = Status.Deleted;

        var repository = new Mock<ICommentRepository>();
        repository.Setup(repo => repo.DeleteAllAsync(1)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<CommentController>>();
        var controller = new CommentController(logger.Object, repository.Object);
    
        // Act
    
        var actual = await controller.DeleteAllComments(1);
    
        // Assert
        Assert.Equal(expected, actual);

    }
}