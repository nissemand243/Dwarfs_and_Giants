 namespace SE_training.Server.Tests.Controllers;

public class MaterialControllerTests
{
    [Fact]
    public async Task CreateMaterial_creates_material_returns_material_and_status()
    {
        // Arrange
        var createMaterialDTO = new CreateMaterialDTO(1,"Lorem 3.", "Lorem material", FileType.Link, "Google.com");
        var materialDTO = new MaterialDTO(1,1,"Lorem the 3.", "Lorem material",FileType.Link, "Google.com");
        var expected = (Status.Created, materialDTO);
        
        var repository = new Mock<IMaterialRepository>();
        repository.Setup(repo => repo.CreateMaterialAsync(createMaterialDTO)).ReturnsAsync(expected);
        var logger = new Mock<ILogger<MaterialController>>();
        var controller = new MaterialController(logger.Object, repository.Object);


        // Act
        var actual = await controller.CreateMaterial(createMaterialDTO);

  
        // Assert
        Assert.Equal(expected.materialDTO, actual.material);
        Assert.Equal(expected.Created, actual.status);
    }

    [Fact]
    public async Task ReadMaterial_given_existing_id_returns_MaterialDTO()
    {
        // Arrange
        var expected = new MaterialDTO(1,2, "Lorem 2.", "Lorem material", FileType.Pdf, "Google.com");
        var logger = new Mock<ILogger<MaterialController>>();
        var repository = new Mock<IMaterialRepository>();
        repository.Setup(repo => repo.ReadAsync(1)).ReturnsAsync(expected);
        var controller = new MaterialController(logger.Object, repository.Object);

        // Act
        var actual = await controller.ReadMaterial(1);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteMaterial_given_none_existing_returns_status_NotFound()
    {
        // Arrrange
        var expected = Status.NotFound;
        var logger = new Mock<ILogger<MaterialController>>();
        var repository = new Mock<IMaterialRepository>();
        repository.Setup(repo => repo.DeleteAsync(21)).ReturnsAsync(expected);
        var controller = new MaterialController(logger.Object, repository.Object);
    
        // Act
        var actual = await controller.DeleteMaterial(21);
    
        // Assert
        Assert.Equal(expected,actual);
    }

    [Fact]
    public async Task ReadAll_returns_materialDTo()
    {
        // Arrange
    
        // Act
    
        // Assert
    }
}