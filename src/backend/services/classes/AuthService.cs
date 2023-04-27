using System.ComponentModel.DataAnnotations;

public class AuthService : IAuthService
{
    private readonly IAuthRepository authRepository;
    private readonly IBcryptTest bcrypt;

    public AuthService(IAuthRepository authRepository, IBcryptTest bcrypt)
    {
        this.authRepository = authRepository;
        this.bcrypt = bcrypt;
    }

    public ResponseRegister login(UserLoginDto userData)
    {
        var VerifyUserRegistration = authRepository.SearchingForEmail(userData);
        if(VerifyUserRegistration == null){
            throw new ValidationException("Email inválido");
        }
        var verifypassword = bcrypt.verify(userData.Senha, VerifyUserRegistration.Password);
        if(verifypassword == false){
            throw new ValidationException("Senha inválida");
        }
        ResponseRegister responsesucess = new ResponseRegister(200, "usuario logado", Guid.NewGuid(), "dfsfsdf");
        return responsesucess;
    }
}