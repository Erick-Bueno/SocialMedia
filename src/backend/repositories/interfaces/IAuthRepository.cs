public interface IAuthRepository
{
    public UserModel SearchingForEmail(UserLoginDto loginData);
    public TokenModel LoggedInBeffore(string email);
    public Task<TokenModel> FindUserEmailWithToken(string jwt);
}