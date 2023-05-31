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
        if(VerifyUserRegistration == null){
            throw new ValidationException("email inválido");
        }
        var verifypassword = bcrypt.verify(userData.senha, VerifyUserRegistration.password);
        if(verifypassword == false){
            throw new ValidationException("Senha inválida");
        }
        var token = jwt.generateJwt(VerifyUserRegistration);
        TokenModel tokenModel = new TokenModel();
        var LoggedInBeffore = authRepository.loggedInBeffore(VerifyUserRegistration.email);
        if(LoggedInBeffore == null){
          tokenModel.email = VerifyUserRegistration.email;
          tokenModel.jwt = token;
          var addUserToken = await tokenRepository.addUserToken(tokenModel);
          ResponseRegister responseSucess = new ResponseRegister(200, "Úsuario logado com sucesso", VerifyUserRegistration.id,token);
          return responseSucess;
        }
        
        var updateToken = await tokenRepository.updateToken(LoggedInBeffore, token);
        ResponseRegister responsesucess = new ResponseRegister(200, "Úsuario logado com sucesso", VerifyUserRegistration.id, token);
        return responsesucess;
    }
     public async Task<ResponseAuth> refreshToken(string Jwt){
        var tokenRegister = await authRepository.findUserEmailWithToken(Jwt);
       
        if(tokenRegister == null){
            throw new ValidationException("token invalido");
        }
        var user = userRepository.userRegistred(tokenRegister.email);
        var newToken = jwt.generateJwt(user);
        var updateToken = await tokenRepository.updateToken(tokenRegister, newToken);
        ResponseAuth responseAuth = new ResponseAuth(newToken);
        return responseAuth;
        
     }
}