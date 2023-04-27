public class ResponseRegister : ResponseGeneric
{
    public Guid Id { get; set; }
    public string Jwt{get; set;}
    public ResponseRegister(int status, string message, Guid id,string jwt ) : base(status, message)
    {
        Id = id;
        Jwt = jwt;
    }
}