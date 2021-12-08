namespace SE_training.Infrastructure;
public abstract class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(75)]
    public string? Name { get; set; }

    [Required]
    [EmailAddressAttribute]
    public string? Email { get; set; }

    public abstract string TypeOfToString();
}