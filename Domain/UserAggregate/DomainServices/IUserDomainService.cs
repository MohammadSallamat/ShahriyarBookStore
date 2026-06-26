using Domain.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate.DomainServices;

public interface IUserDomainService
{
    bool PhoneNumberIsExist(string phoneNumber);
    bool EmailIsExist(string email);
}
