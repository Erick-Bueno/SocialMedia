using System.ComponentModel.DataAnnotations;
public class CommentDto
{
    [Required(ErrorMessage ="Informe um comentario")]
    public string comment { get; set; }
    public Guid userId { get; set; }
    public Guid postId { get; set; }
}