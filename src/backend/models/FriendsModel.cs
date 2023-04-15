using System.ComponentModel.DataAnnotations.Schema;

public class FriendsModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id {get; set;}

    public Guid user_id {get; set;}
    public Guid user_id_2 {get; set;}
   
    public UserModel userModel{get;set;}
  
    public UserModel userModel2{get;set;}
}