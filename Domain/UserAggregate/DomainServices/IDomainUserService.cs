﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate.DomainServices;

public interface IDomainUserService
{
    bool PhoneNumberIsExist(string phone);
    bool EmailIsExist(string email);
}
