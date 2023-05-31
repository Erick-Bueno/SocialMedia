public class ResponseUserFinded : ResponseGeneric
{
   
    public string Name { get; set; }
    public string Profileimage { get; set; }
    public int Friendsquantity { get; set; }

    public ResponseUserFinded(int status, string message, string Name,string Profileimage,int Friendsquantity ) : base(status, message)
    {
        this.Name = Name;
        this.Profileimage = Profileimage;
        this.Friendsquantity = Friendsquantity;
    }
}