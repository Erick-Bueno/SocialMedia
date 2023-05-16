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
        var VerifyUserRegistration = authRepository.SearchingForEmail(userData);
        if(VerifyUserRegistration == null){
            throw new ValidationException("Email inválido");
        }
        var verifypassword = bcrypt.verify(userData.Senha, VerifyUserRegistration.Password);
        if(verifypassword == false){
            throw new ValidationException("Senha inválida");
        }
        var token = jwt.generateJwt(VerifyUserRegistration);
        TokenModel tokenModel = new TokenModel();
        var LoggedInBeffore = authRepository.LoggedInBeffore(VerifyUserRegistration.Email);
        if(LoggedInBeffore == null){
          tokenModel.Email = VerifyUserRegistration.Email;
          tokenModel.jwt = token;
          var addUserToken = await tokenRepository.addUserToken(tokenModel);
          ResponseRegister responseSucess = new ResponseRegister(200, "Úsuario logado com sucesso", VerifyUserRegistration.id,token);
          return responseSucess;
        }
        
        var updateToken = await tokenRepository.UpdateToken(LoggedInBeffore, token);
        ResponseRegister responsesucess = new ResponseRegister(200, "Úsuario logado com sucesso", VerifyUserRegistration.id, token);
        return responsesucess;
    }
     public async Task<ResponseAuth> RefreshToken(string Jwt){
        var tokenRegister = await authRepository.FindUserEmailWithToken(Jwt);
        if(tokenRegister == null){
            throw new ValidationException("token invalido");
        }
        var user = userRepository.user_registred(tokenRegister.Email);
        var newToken = jwt.generateJwt(user);
        var updateToken = tokenRepository.UpdateToken(tokenRegister, newToken);
        ResponseAuth responseAuth = new ResponseAuth(newToken);
        return responseAuth;
        
     }
}