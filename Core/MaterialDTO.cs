namespace SE_training.Core
{
    public record MaterialDto(int Id, String Name, string Description, FileType FileType);
    public record MaterialCreateDto
    {
        public string Name { get; init; }
        public string Description { get; init; } 
        public FileType FileType { get; init; }
        [Url]
        public string FilePath { get; init; }

        public ICollection<string>? Tags { get; init; } 

    }
    public record MaterialUpdateDto : MaterialCreateDto
    {
        public int Id { get; init; }
        public ICollection<string>? Tags { get; init; }  

        public ICollection<Comment>? Comments { get; init; } 

        public IDictionary<int, int>? Ratings { get; init; }     
    }
    
}
