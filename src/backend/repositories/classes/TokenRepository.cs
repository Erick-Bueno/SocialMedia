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

    public async Task<TokenModel> UpdateToken(TokenModel token, string jwt)
    {
      
        token.jwt = jwt;

        await _context.SaveChangesAsync();
        return token;
    }
}