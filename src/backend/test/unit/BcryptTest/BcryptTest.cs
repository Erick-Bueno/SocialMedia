public class BcryptTest : IBcryptTest
{
    public bool verify(string password, string hashPassword)
    {
       return BCrypt.Net.BCrypt.Verify(password,hashPassword);
    }
}