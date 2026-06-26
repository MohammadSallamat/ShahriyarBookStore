using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersApp
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool EmailIsExist(string email)
        {
            return _userRepository.Exists(x => x.Email == email);
        }

        public bool PhoneNumberIsExist(string phoneNumber)
        {
            return _userRepository.Exists(y => y.PhoneNumber.Value == phoneNumber);
        }
    }
}
