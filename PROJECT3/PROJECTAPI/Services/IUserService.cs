using PROJECTAPI.Models;

namespace PROJECTAPI.Services
{
    public interface IUserService
    {
        string SignUp(UserModel user);
        UserModel Login(LoginModel user);
        void Logout();

    }
}
