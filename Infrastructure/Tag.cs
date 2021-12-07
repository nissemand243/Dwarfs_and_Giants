namespace SE_training.Infrastructure;

public class Tag
{
    private Tag() { }
    public Tag(string tagName)
    {
        TagName = tagName;
    }
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public string TagName { get; set; }

    
}