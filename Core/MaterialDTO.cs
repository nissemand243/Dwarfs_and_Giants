namespace SE_training.Core
{
    public record MaterialDto(int Id, String Name, string Description, FileType FileType, ICollection<string>? Tags);
    public record MaterialCreateDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; } 
        [Required]
        public FileType FileType { get; init; }
        
        [Required, Url]
        
        public string FilePath { get; init; }

        public ICollection<string>? Tags { get; init; } 

    }
    public record MaterialUpdateDto : MaterialCreateDto
    {
        [Required]
        public int Id { get; init; }
        public ICollection<string>? Tags { get; init; }  

        public ICollection<Comment>? Comments { get; init; } 

        public IDictionary<int, int>? Ratings { get; init; }     
    }
    
}
