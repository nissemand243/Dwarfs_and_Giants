namespace SE_training.Infrastructure;

public class SearchEngine : ISEarchEngine
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

    public async Task<ICollection<DetailsMaterialDTO>> SearchMaterialsAsync(string searchString)
    {
        var matches = new List<DetailsMaterialDTO>();

        var nameMatches = await SearchMaterialsByNameAsync(searchString);
        foreach (var nameMatch in nameMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == nameMatch.Id))
            {
                matches.Add(nameMatch);
            }
        }

        var tagMatches = await SearchMaterialsByTagsAsync(searchString);
        foreach (var tagMatch in tagMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == tagMatch.Id))
            {
                matches.Add(tagMatch);
            }
        }

        var authorMatches = await SearchMaterialsByAuthorAsync(searchString);
        foreach (var authorMatch in authorMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == authorMatch.Id))
            {
                matches.Add(authorMatch);
            }
        }

        return matches;
    }

    public async Task<ICollection<DetailsMaterialDTO>> SearchMaterialsByNameAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.GetAsync();

        var matches = new List<DetailsMaterialDTO>();
        foreach (var material in materials)
        {
            if (material.Name != null && material.Name.ToLower().Contains(searchString))
            {
                matches.Add(await GetDetailedMaterialByIdAsync(material.Id));
            }
        }
        return matches;
    }

    public async Task<ICollection<DetailsMaterialDTO>> SearchMaterialsByTagsAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var tags = await _tagRepo.ReadAsync();

        var matches = new List<DetailsMaterialDTO>();

        foreach (var tag in tags)
        {
            if (tag.TagName != null && tag.TagName.ToLower().Contains(searchString))
            {
                if (!matches.Any(material => material.Id == tag.MaterialId))
                {
                    var material = await _materialRepo.GetAsync(tag.MaterialId);
                    matches.Add(await GetDetailedMaterialByIdAsync(material.Id));
                }
            }
        }
        return matches;
    }

    public async Task<ICollection<DetailsMaterialDTO>> SearchMaterialsByAuthorAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.GetAsync();

        var matches = new List<DetailsMaterialDTO>();
        foreach (var material in materials)
        {
            var user = await _userRepo.ReadAsync(material.AuthorId);
            if (user.Name != null && user.Name.ToLower().Contains(searchString))
            {
                matches.Add(await GetDetailedMaterialByIdAsync(material.Id));
            }
        }
        return matches;
    }

    public async Task<DetailsMaterialDTO> GetDetailedMaterialByIdAsync(int materialId)
    {
        var material = await _materialRepo.GetAsync(materialId);

        var readTags = await _tagRepo.ReadAsync(material.Id);
        var readComments = await _commentRepo.ReadAsync(material.Id);
        var readRatings = await _ratingRepo.ReadAsync(material.Id);

        var tags = new List<TagDTO>();
        foreach (var tag in readTags)
        {
            tags.Add(tag);
        }

        var comments = new List<CommentDTO>();
        foreach (var comment in readComments)
        {
            comments.Add(comment);
        }

        //calculate the adverage rating of the material
        double adv = 0;
        int nRatings = 0;
        foreach (var rating in readRatings)
        {
            adv += rating.Value;
            nRatings++;
        }
        adv /= nRatings;

        return new DetailsMaterialDTO(material.Id, material.AuthorId, material.Name, material.Description, material.FileType == null ? null : material.FileType.ToString(), material.FilePath, tags, comments, adv);
    }

    public async Task<ICollection<DetailsMaterialDTO>> GetRelatedMaterialsByTags(int materialId)
    {
        var tagsOnMaterial = await _tagRepo.ReadAsync(materialId);
        var readAllTags = await _tagRepo.ReadAsync();
        var allTags = new List<TagDTO>();
        foreach (var tag in readAllTags)
        {
            allTags.Add(tag);
        }

        var matches = new List<DetailsMaterialDTO>();
        foreach (var tag in tagsOnMaterial)
        {
            var matchingTags = allTags.Where(t => t.TagName == tag.TagName);
            foreach (var matchingTag in matchingTags)
            {
                if (!matches.Any(material => material.Id == matchingTag.MaterialId))
                {
                    var material = await _materialRepo.GetAsync(matchingTag.MaterialId);
                    matches.Add(await GetDetailedMaterialByIdAsync(material.Id));
                }
            }
        }
        return matches;
    }
}