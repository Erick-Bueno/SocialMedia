public abstract class ResponseGeneric
{
    public int Status { get; set; }
    public string Message{get; set;}

    public ResponseGeneric(int status, string message){
        Status = status;
        Message = message;
    }
}