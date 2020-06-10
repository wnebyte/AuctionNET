using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.DAL.Repositories;
using AuctionCore.Models.UserModel;

namespace AuctionCore.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService() =>
            _repo = new UserRepository();

        public bool Exists(string username) =>
            _repo.Exists(username);

        public bool Exists(string username, out User userOut)
        {
            userOut = _repo.Get(username);
            return _repo.Exists(username);
        }

        public void Insert(User userIn) =>
            _repo.Insert(userIn);

        public List<User> Get() =>
            _repo.Get();

        public void Delete(User user) =>
            _repo.Delete(user);
    }
}
