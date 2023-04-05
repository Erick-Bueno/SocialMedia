using System.ComponentModel.DataAnnotations.Schema;

public class CommentModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id {get; set;}
    public string comment {get;set;}

    public Guid User_id {get; set;}
    public Guid Posts_id {get; set;}

    public UserModel userModel{get;set;}
    public PostModel postModel{get;set;}
}