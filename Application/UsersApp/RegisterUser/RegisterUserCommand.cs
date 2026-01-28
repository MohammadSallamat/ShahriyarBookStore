using Application.CommonApplication;
using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersApp.RegisterUser;

public record RegisterUserCommand(string PhoneNumber, string Password) : IBaseCommand;
