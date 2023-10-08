public class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _context;

    public TokenRepository(AppDbContext context)
    {
        _context = context;
    }

    async public Task<TokenModel> addUserToken(TokenModel token)
    {
        var addUserToken = await _context.Token.AddAsync(token);
        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<TokenModel> updateToken(TokenModel token, string jwt)
    {

        token.jwt = jwt;
        await _context.SaveChangesAsync();
        return token;
    }
    public TokenModel findToken(string email)
    {
        var findedToken = _context.Token.Where(t => t.email == email).FirstOrDefault();
        return findedToken;
    }
    public TokenModel findUserEmailWithToken(string jwt)
    {
        var tokenRegister = _context.Token.Where(tk => tk.jwt == jwt).FirstOrDefault();
        return tokenRegister;
    }

}