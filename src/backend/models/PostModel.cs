using System.ComponentModel.DataAnnotations.Schema;

public class PostModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime datePost { get; set; } = DateTime.UtcNow;
    public string contentPost { get; set; }
    public Guid userId {get; set;}
    public Int32 totalLikes {get; set;}
    public Int32 totalComments{get; set;}
    public UserModel user {get; set;}
    public List<PostImagesModel> postsimages {get; set;}
    public List<LikesModel> postLikes {get; set;}
    public List<CommentModel> commentsPost{get;set;}
}