public interface ITokenRepository
{
    public Task<TokenModel> addUserToken(TokenModel token);

    public Task<TokenModel> UpdateToken(TokenModel token, string jwt);
}