namespace SE_training.Core;

public interface IBasicController
{
    Task<IReadOnlyCollection<MaterialDTO>> Search(string searchInput);
}
