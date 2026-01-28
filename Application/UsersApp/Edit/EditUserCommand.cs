using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersApp.Edit;

public class EditUserCommand : IBaseCommand
{
    public EditUserCommand(long userId, IFormFile? avatar, string name, string family, PhoneNumber phoneNumber, string email, Gender gender)
    {
        UserId = userId;
        Avatar = avatar;
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }
    public long UserId { get; set; }
    public IFormFile? Avatar { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Gender Gender { get; private set; }
}
