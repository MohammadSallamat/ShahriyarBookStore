using Application.CommonApplication;
using Application.Roles.Edit;
using Domain.ProductAggregate;
using Domain.RoleAggregate;
using Domain.RoleAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.Edit;
public record EditRoleCommand(
    long RoleId,
    string Title,
    List<Permission>? AddPermissions = null,
    List<Permission>? RemovePermissions = null
) : IBaseCommand;

