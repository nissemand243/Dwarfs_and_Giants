namespace SE_Training.Infrastructure;
public abstract class User
{
    
   [Required]
   [StringLength(75)]
   public string Name { get; set; } 


   [EmailAddressAttribute]
   public string Email { get; set; }
}