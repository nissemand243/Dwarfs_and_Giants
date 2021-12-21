namespace SE_training.Server;

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

    public async Task<IReadOnlyCollection<MaterialDTO>> SearchAsync(string searchString)
    {
        var matches = new List<MaterialDTO>();

        var nameMatches = await SearchByNameAsync(searchString);
        foreach (var nameMatch in nameMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == nameMatch.Id))
            {
                matches.Add(nameMatch);
            }
        }

        var descriptionMatches = await SearchByDescriptionAsync(searchString);
        foreach (var descriptionMatch in descriptionMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == descriptionMatch.Id))
            {
                matches.Add(descriptionMatch);
            }
        }

        var tagMatches = await SearchByTagsAsync(searchString);
        foreach (var tagMatch in tagMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == tagMatch.Id))
            {
                matches.Add(tagMatch);
            }
        }

        var authorMatches = await SearchByAuthorAsync(searchString);
        foreach (var authorMatch in authorMatches)
        {
            if (!matches.Any(matchedMaterial => matchedMaterial.Id == authorMatch.Id))
            {
                matches.Add(authorMatch);
            }
        }

        return matches.AsReadOnly();
    }

    public async Task<IList<MaterialDTO>> SearchByNameAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.ReadAllAsync();

        var matches = new List<MaterialDTO>();
        foreach (var material in materials)
        {
            if (material.Name != null && material.Name.ToLower().Contains(searchString))
            {
                matches.Add(material);
            }
        }
        return matches;
    }

    public async Task<IList<MaterialDTO>> SearchByDescriptionAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.ReadAllAsync();

        var matches = new List<MaterialDTO>();
        foreach (var material in materials)
        {
            if (material.Description != null && material.Description.ToLower().Contains(searchString))
            {
                matches.Add(material);
            }
        }
        return matches;
    }

    public async Task<IList<MaterialDTO>> SearchByTagsAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var tags = await _tagRepo.ReadAllAsync();

        var matches = new List<MaterialDTO>();

        foreach (var tag in tags)
        {
            if (tag.TagName != null && tag.TagName.ToLower().Contains(searchString))
            {
                if (!matches.Any(material => material.Id == tag.MaterialId))
                {
                    matches.Add(await _materialRepo.ReadAsync(tag.MaterialId));
                }
            }
        }
        return matches;
    }

    public async Task<IList<MaterialDTO>> SearchByAuthorAsync(string searchString)
    {
        searchString = searchString.ToLower();

        var materials = await _materialRepo.ReadAllAsync();

        var matches = new List<MaterialDTO>();
        foreach (var material in materials)
        {
            var user = await _userRepo.ReadAsyncId(material.AuthorId);
            if (user.Name != null && user.Name.ToLower().Contains(searchString))
            {
                matches.Add(material);
            }
        }
        return matches;
    }

    public async Task<DetailsMaterialDTO?> GetDetailedMaterialByIdAsync(int materialId)
    {
        var material = await _materialRepo.ReadAsync(materialId);

        if (material == null)
        {
            return null;
        }

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
        adv /= (nRatings == 0 ? 1 : nRatings);

        return new DetailsMaterialDTO(material.Id, material.AuthorId, material.Name, material.Description, material.FileType, material.FilePath, tags, comments, adv);
    }

    public async Task<IReadOnlyCollection<MaterialDTO>> GetRelatedMaterialsByTagsAsync(int materialId)
    {
        var tagsOnMaterial = await _tagRepo.ReadAsync(materialId);
        var readAllTags = await _tagRepo.ReadAllAsync();
        var allTags = new List<TagDTO>();
        foreach (var tag in readAllTags)
        {
            allTags.Add(tag);
        }

        var matches = new List<MaterialDTO>();
        foreach (var tag in tagsOnMaterial)
        {
            var matchingTags = allTags.Where(t => t.TagName == tag.TagName);
            foreach (var matchingTag in matchingTags)
            {
                if (matchingTag.MaterialId != materialId && !matches.Any(material => material.Id == matchingTag.MaterialId))
                {
                    matches.Add(await _materialRepo.ReadAsync(matchingTag.MaterialId));
                }
            }
        }

        return matches.AsReadOnly();
    }
}
