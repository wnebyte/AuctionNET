using System.Collections.Generic;
using AuctionCore.Data.Repositories;
using AuctionCore.Models.User;

namespace AuctionCore.Data.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public List<User> GetAll() =>
            _repo.Get();

        public User Get(string username) =>
            _repo.Get(username);

        public bool Insert(User user)
        {
            _repo.Insert(user);
            return true;
        }

        public bool Update(User user)
        {
            _repo.Update(user.Username, user);
            return true;
        }

        public bool Delete(User user)
        {
            _repo.Delete(user);
            return true;
        }

        public bool Exists(string username) =>
            _repo.Exists(username);

        public bool Exists(string username, out User user)
        {
            user = _repo.Get(username);
            return _repo.Exists(username);
        }
    }
}
