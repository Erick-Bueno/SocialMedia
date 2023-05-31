using System.ComponentModel.DataAnnotations.Schema;

public class FriendsModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id {get; set;}

    public Guid userId {get; set;}
    public Guid userId2 {get; set;}
   
    public UserModel userModel{get;set;}
  
    public UserModel userModel2{get;set;}
}