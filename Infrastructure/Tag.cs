namespace SE_training.Infrastructure;

public class Tag
{
    public int Id { get; init; }
    public int MaterialId { get; init; }
    public string TagName { get; set; }

    public Tag(string tagName)
    {
        TagName = tagName;
    }
}