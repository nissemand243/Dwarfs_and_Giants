

namespace SE_training.Server.Tests.Controllers;

public class CommentControllerTests
{
    [Fact]
    public async Task CreateComment_creates_comment_returns_comment_and_status()
    {
        // Arrange
        var createCommentDto = new CreateCommentDTO(1,1,"Lorem Ipsum");
        var commentDTO = new CommentDTO(1,1,1,"Lorem Ipsum");
        var Expected = (Status.Created, commentDTO);

        var mockRepository = new Mock<ICommentRepository>();
        mockRepository.Setup(repo => repo.CreateAsync(createCommentDto)).ReturnsAsync(Expected);
        var mockLogger = new Mock<ILogger<CommentController>>();
        

        var controller = new CommentController(mockLogger.Object, mockRepository.Object);

        // Act
        var result = await controller.CreateComment(createCommentDto);

        // Assert
        Assert.Equal(Expected.commentDTO, result.comment);
        Assert.Equal(Expected.Created, result.status);       
    }
}