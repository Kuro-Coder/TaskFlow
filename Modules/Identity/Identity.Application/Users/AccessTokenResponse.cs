using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Users;

public sealed record AccessTokenResponse(
    string AccessToken,
    DateTime ExpiresAtUtc);
