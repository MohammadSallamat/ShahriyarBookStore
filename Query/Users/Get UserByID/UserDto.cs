using Domain.UserAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Users.Get_UserByID
{
    internal class UserDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AvatarName { get; set; }
        public Gender Gender { get; set; }
    }
}
