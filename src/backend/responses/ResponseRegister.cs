public class ResponseRegister
{
    public Guid Id { get; set; }
    //public int MyProperty { get; set; }
    public int Status { get; set; }
    public string Message{get; set;}

    public ResponseRegister(Guid id, int status, string message){
        Id = id;
        Status = status;
        Message = message;

    }
}