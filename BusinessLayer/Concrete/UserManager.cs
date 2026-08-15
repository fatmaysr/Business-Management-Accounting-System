using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Web.Helpers;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            // Şifreyi hiçbir zaman düz metin olarak DB'ye yazmıyoruz.
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _userDal.Insert(user);
        }

        public void Update(User user)
        {
            var existing = _userDal.Get(x => x.UserId == user.UserId);

            // Kullanıcı formda şifreyi değiştirmediyse (hash aynen geri geldiyse)
            // tekrar hashlemiyoruz; değiştirdiyse yeni şifreyi hashliyoruz.
            if (existing == null || user.Password != existing.Password)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _userDal.Update(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public User GetById(int id)
        {
            return _userDal.Get(x => x.UserId == id);
        }

        public List<User> GetList()
        {
            return _userDal.List();
        }

   
        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = _userDal.Get(x => x.UserName == username);
            if (user == null)
                return null;

            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return _userDal.Get(x => x.Email == email);
        }
    }
}
