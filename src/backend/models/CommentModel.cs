using System.ComponentModel.DataAnnotations.Schema;

public class CommentModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id {get; set;}
    public string comment {get;set;}

    public Guid userId {get; set;}
    public Guid postId {get; set;}

    public UserModel userModel{get;set;}
    public PostModel postModel{get;set;}
}