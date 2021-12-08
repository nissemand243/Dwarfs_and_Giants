namespace SE_training.Infrastructure;
public abstract class User
{
    private User() { }
    public User(string name, string email) 
    {
        Name = name;
        Email = email;
    }
    public int Id { get; set; }


    [Required]
    [StringLength(75)]
    public string Name { get; set; }


    [EmailAddressAttribute]
    public string Email { get; set; }

    public abstract string TypeOfToString();
}