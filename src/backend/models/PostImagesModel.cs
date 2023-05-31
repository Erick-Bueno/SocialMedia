using System.ComponentModel.DataAnnotations.Schema;

public class PostImagesModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    public string imgUrl {get; set;}
    public Guid postId {get; set;}
    public PostModel posts {get; set;}
}