namespace SE_training.Infrastructure;
public abstract class User
{
    public int UserId { get; set; }


    [Required]
    [StringLength(75)]
    public string? Name { get; set; }


    [EmailAddressAttribute]
    public string? Email { get; set; }

    public abstract string TypeOfToString();
}