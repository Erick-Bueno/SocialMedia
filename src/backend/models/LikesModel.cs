using System.ComponentModel.DataAnnotations.Schema;

public class LikesModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    public Guid Posts_id {get; set;}
    public Guid Users_id {get; set;}

    public PostModel postModel{get; set;}
    public UserModel userModel{get; set;}
}