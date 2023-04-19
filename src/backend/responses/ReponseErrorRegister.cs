public class ReponseErrorRegister
{
    public int Status { get; set; }
    public string Message{get; set;}

    public ReponseErrorRegister(int status, string message){
        Status = status;
        Message = message;
    }
}