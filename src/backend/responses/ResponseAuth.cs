public class ResponseAuth 
{

    public string Jwt{get; set;}
    public ResponseAuth(string jwt )
    {
        Jwt = jwt;
    }
}