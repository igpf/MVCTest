using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSM.Business.Interfaces;
using OSM.Data.Entities;
using OSM.Data.Repositories;

namespace OSM.Business.Services
{
    public class UsersBusinessService : IUsersBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Users> _usersRepository;


        public UsersBusinessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = _unitOfWork.RepositoryFor<Users>();
        }

        public List<Users> GetAll() => _usersRepository.GetItems().ToList();

        private bool Exists(Users users)
        {
            return _usersRepository.GetItem(x => x.UserName == users.UserName) != null;
        }

        public void AddUser(Users users)
        {
            if (!Exists(users))
            {
                _usersRepository.Add(users);
                _unitOfWork.SaveChanges();
            }
        }
    }
}
