using System.ComponentModel.DataAnnotations.Schema;

public class LikesModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    public Guid postId {get; set;}
    public Guid userId {get; set;}

    public PostModel postModel{get; set;}
    public UserModel userModel{get; set;}
}