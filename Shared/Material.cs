namespace SE_training.Client.Shared;

public class Material
{

    public int Id { get; set; }
    public string Title { get; set; }
    
    public string Decription {get;set; }
    public int Reating {get;set; }
    
    //list of tags
    public List<string> Tags = new List<string>()
                    {
                        "Docker",
                        "C#",
                        "Blazor",
                        "BDSA"                    
                    };

  
}