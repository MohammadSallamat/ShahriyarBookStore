using Application.CommonApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersApp.DeleteAdress;

public record DeleteUserAddressCommand(long UserId, long AddressId) : IBaseCommand;
