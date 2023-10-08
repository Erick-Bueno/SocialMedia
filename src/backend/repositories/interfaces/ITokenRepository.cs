public interface ITokenRepository
{
    public Task<TokenModel> addUserToken(TokenModel token);

    public Task<TokenModel> updateToken(TokenModel token, string jwt);
    public TokenModel findToken (string email);

    public TokenModel findUserEmailWithToken(string jwt);
}