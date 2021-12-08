namespace SE_training.Infrastructure;

public class SearchEngine
{
    private readonly IUserRepository _userRepo;
    private readonly IMaterialRepository _materialRepo;
    private readonly ITagRepository _tagRepo;
    private readonly ICommentRepository _commentRepo;
    private readonly IRatingRepository _ratingRepo;


    public SearchEngine(IUserRepository userRepo, IMaterialRepository materialRepo, ITagRepository tagRepo, ICommentRepository commentRepo, IRatingRepository ratingRepo)
    {
        _userRepo = userRepo;
        _materialRepo = materialRepo;
        _tagRepo = tagRepo;
        _commentRepo = commentRepo;
        _ratingRepo = ratingRepo;
    }

    public async Task<IEnumerable<MaterialDTO>> SearchMaterialsByName(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.GetAsync();
        var matches = new List<MaterialDTO>();

        foreach (var material in materials)
        {
            if (material.Name != null && material.Name.ToLower().Contains(searchString))
            {
                matches.Add(new MaterialDTO(material.Id, material.AuthorId, material.Name, material.Description, material.FileType == null ? null : material.FileType.ToString(), material.FilePath));
            }
        }

        return matches;
    }

    public async Task<IEnumerable<MaterialDTO>> SearchMaterialsByTags(string searchString)
    {
        searchString = searchString.ToLower();


        var tags = await _tagRepo.ReadAsync();
        var matches = new List<MaterialDTO>();

        foreach (var tag in tags)
        {
            if (tag.TagName != null && tag.TagName.ToLower().Contains(searchString))
            {
                if (!matches.Any(m => m.Id == tag.MaterialId))
                {
                    var material = await _materialRepo.GetAsync(tag.MaterialId);
                    matches.Add(new MaterialDTO(material.Id, material.AuthorId, material.Name, material.Description, material.FileType == null ? null : material.FileType.ToString(), material.FilePath));
                }
            }
        }

        return matches;
    }

    public async Task<IEnumerable<MaterialDTO>> SearchMaterialsByAuthor(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.GetAsync();
        var matches = new List<MaterialDTO>();

        foreach (var material in materials)
        {
            var user = await _userRepo.ReadAsync(material.AuthorId);
            if (user.Name != null && user.Name.ToLower().Contains(searchString))
            {
                matches.Add(new MaterialDTO(material.Id, material.AuthorId, material.Name, material.Description, material.FileType == null ? null : material.FileType.ToString(), material.FilePath));
            }
        }

        return matches;
    }

    /*public async Task<DetailsMaterialDTO> GetMaterialById(int id)
    {
        var material = await _materialRepo.GetAsync(id);

        var tags = await _tagRepo.ReadAsync(material.Id);
        var comments = await _commentRepo.ReadAsync(material.Id);

        var ratings = await _ratingRepo.ReadAsync(material.Id);
        double adv = 0;
        int nRatings = 0;
        foreach (var rating in ratings)
        {
            adv += rating.Value;
            nRatings++;
        }
        adv /= nRatings;

        return new DetailsMaterialDTO
    }*/

}