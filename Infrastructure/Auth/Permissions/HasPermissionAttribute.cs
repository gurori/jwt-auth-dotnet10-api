using Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Auth.Permissions;

public sealed class HasPermissionAttribute(Permission permission)
    : AuthorizeAttribute(policy: permission.ToString()) { }
