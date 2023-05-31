public interface IAuthRepository
{
    public UserModel searchingForEmail(UserLoginDto loginData);
    public TokenModel loggedInBeffore(string email);
    public Task<TokenModel> findUserEmailWithToken(string jwt);
}