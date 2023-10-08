public class ResponseRegister : ResponseGeneric
{
    public string Jwt{get; set;}
    public ResponseRegister(int status, string message,string jwt ) : base(status, message)
    {
        Jwt = jwt;
    }
}