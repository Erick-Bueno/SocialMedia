using System.ComponentModel.DataAnnotations.Schema;

public class RequestsModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    public StatusEnum status {get; set;}
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime RequestDate {get; set;} =DateTime.UtcNow;

    public Guid Requester_id {get; set;}
    public Guid Receiver_id{get; set;}
    
    public UserModel userModel {get; set;}
  
    public UserModel UserModel2 { get; set;}

}