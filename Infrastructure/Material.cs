namespace SE_training.Infrastructure;

public class Material
{
    public int id {get; set;}
    [StringLength(100)]
    public string? name {get; set;}
    public string? description {get; set;}
    public string? filePath {get; set;} 
    public string? fileType {get; set;}
    [StringLength(50)]
    public string[]? tags {get; set;}
    public Comment[]? comments {get; set;}
    public Rating[]? ratings {get; set;}
}