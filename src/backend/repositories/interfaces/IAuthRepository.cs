public interface IAuthRepository
{
    public UserModel searchingForEmail(UserLoginDto loginData);
}