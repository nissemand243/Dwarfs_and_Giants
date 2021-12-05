namespace SE_training.Infrastructure;

public class Material
{
    public int MaterialId { get; set; }
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public FileType FileType { get; set; }
    public ICollection<string>? Tags { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public IDictionary<int, int>? Ratings { get; set; } //UserId to ratingValue

    [Url]
    public string? FilePath { get; set; }
}