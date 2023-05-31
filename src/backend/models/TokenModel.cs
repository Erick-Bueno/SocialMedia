using System.ComponentModel.DataAnnotations.Schema;

public class TokenModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id {get; set;}
    public string jwt {get; set;}
    public string email{get;set;}
}