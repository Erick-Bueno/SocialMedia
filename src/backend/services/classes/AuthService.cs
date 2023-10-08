using System.ComponentModel.DataAnnotations;

public class AuthService : IAuthService
{
    private readonly IAuthRepository authRepository;
    private readonly ITokenRepository tokenRepository;
    private readonly IBcryptTest bcrypt;
    private readonly IUserRepository userRepository;
    private readonly Ijwt jwt;

    public AuthService(IAuthRepository authRepository, IBcryptTest bcrypt, ITokenRepository tokenRepository, Ijwt jwt, IUserRepository userRepository)
    {
        this.authRepository = authRepository;
        this.bcrypt = bcrypt;
        this.tokenRepository = tokenRepository;
        this.jwt = jwt;
        this.userRepository = userRepository;
    }

    public async Task<ResponseRegister> login(UserLoginDto userData)
    {
        var VerifyUserRegistration = authRepository.searchingForEmail(userData);
        if (VerifyUserRegistration == null)
        {
            throw new ValidationException("email inválido");
        }
        var verifypassword = bcrypt.verify(userData.senha, VerifyUserRegistration.password);
        if (verifypassword == false)
        {
            throw new ValidationException("Senha inválida");
        }
        var token = jwt.generateJwt(VerifyUserRegistration);
        TokenModel tokenModel = new TokenModel();
        var findedToken = tokenRepository.findToken(VerifyUserRegistration.email);
        var updateToken = await tokenRepository.updateToken(findedToken, token);
        ResponseRegister responsesucess = new ResponseRegister(200, "Úsuario logado com sucesso", token);
        return responsesucess;
    }
    public async Task<ResponseAuth> refreshToken(string Jwt)
    {
        var tokenRegister = tokenRepository.findUserEmailWithToken(Jwt);

        if (tokenRegister == null)
        {
            throw new ValidationException("token invalido");
        }
        var user = userRepository.userRegistred(tokenRegister.email);
        var newToken = jwt.generateJwt(user);
        var updateToken = await tokenRepository.updateToken(tokenRegister, newToken);
        ResponseAuth responseAuth = new ResponseAuth(newToken);
        return responseAuth;

    }
}