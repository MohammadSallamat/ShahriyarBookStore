using Domain.UserAggregate.Enums;
using Query.Common.Domain;
using Query.Common.Query.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Users.List_of_users
{
    internal class UserListItemDto : BaseDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? AvatarName { get; set; }
        public Gender Gender { get; set; }
        public string GenderTitle =>
            Gender.GetDisplayName();
    }
}
