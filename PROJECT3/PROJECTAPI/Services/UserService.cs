using PROJECTAPI.Data;
using PROJECTAPI.Models;

namespace PROJECTAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserModel Login(LoginModel login)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == login.UserId && x.Password == login.Password);
            return user;
        }

        public void Logout()
        {
            // no code
        }

        public string SignUp(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return "ok";
        }
    }
}
