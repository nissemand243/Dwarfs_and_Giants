namespace SE_training.Infrastructure;

public class Tag
{
    public int Id { get; set; }
    public int MaterialId { get; set; }

    [StringLength(50)]
    public string? TagName { get; set; }
}