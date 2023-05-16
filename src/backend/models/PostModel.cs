using System.ComponentModel.DataAnnotations.Schema;

public class PostModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime DatePost { get; set; } = DateTime.UtcNow;
    public string ContentPost { get; set; }
    public Guid User_id {get; set;}
    public Int32 Tot_likes {get; set;}
    public Int32 tot_comments{get; set;}
    public UserModel user {get; set;}
    public List<PostImagesModel> postsimages {get; set;}
    public List<LikesModel> postLikes {get; set;}
    public List<CommentModel> commentsPost{get;set;}
}