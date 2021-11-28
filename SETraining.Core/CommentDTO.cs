namespace SETraining.Core;

public record CommentCreateDTO
{

    public int id {get; set; }
    [Required, StringLength(50)]
    public string? comment {get; set;}
}

public record CommentDTO(int id, string comment);

public record CommentUpdateDTO(int id, string comment);