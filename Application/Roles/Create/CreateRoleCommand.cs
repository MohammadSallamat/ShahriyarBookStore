using Application.CommonApplication;
using Domain.ProductAggregate;
using Domain.RoleAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.Create;

public record CreateRoleCommand(string Title) : IBaseCommand;

