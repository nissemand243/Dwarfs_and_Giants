namespace SE_training.Infrastructure;
public class User
{
    public int UserId { get; set; }


    [Required]
    [StringLength(75)]
    public string? Name { get; set; }


    [EmailAddressAttribute]
    public string? Email { get; set; }
}