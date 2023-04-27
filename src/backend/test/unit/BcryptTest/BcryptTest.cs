public class BcryptTest : IBcryptTest
{
    public bool verify(string password, string HashPassword)
    {
       return BCrypt.Net.BCrypt.Verify(password,HashPassword);
    }
}