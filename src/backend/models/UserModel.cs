using System.ComponentModel.DataAnnotations.Schema;

public class UserModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    public string userName{get;set;}
    public string telephone{get;set;}
    public string email{get;set;}
    public string password{get;set;}
    public string? userPhoto{get; set;}

    public List<PostModel> posts{get; set;}
    public List<LikesModel> userlikes {get;set;}
    public List<CommentModel> userComments{get;set;}
    public List<FriendsModel> UsersFriends{get;set;}
    public List<FriendsModel> UsersFriends2{get;set;}

    public List<RequestsModel> usersRequests {get;set;}
    public List<RequestsModel> usersRequests2{get; set;}
}

