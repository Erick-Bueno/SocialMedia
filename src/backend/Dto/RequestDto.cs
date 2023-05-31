public class RequestDto
{
    public StatusEnum status { get; set; }
    public Guid requesterId {get; set;}
    public Guid receiverId{get; set;}

}