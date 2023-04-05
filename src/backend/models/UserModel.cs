using System.ComponentModel.DataAnnotations.Schema;

public class UserModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    public string UserName{get;set;}
    public string Telephone{get;set;}
    public string Email{get;set;}
    public string Password{get;set;}
    public string? User_Photo{get; set;}

    public List<PostModel> posts{get; set;}
    public List<LikesModel> userlikes {get;set;}
    public List<CommentModel> userComments{get;set;}
    public List<FriendsModel> UsersFriends{get;set;}
    public List<FriendsModel> UsersFriends2{get;set;}

    public List<RequestsModel> usersRequests {get;set;}
    public List<RequestsModel> usersRequests2{get; set;}
}

