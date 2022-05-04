using System;

namespace Inclub.Api.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();

        string GetUserIP();

        Guid GetUserGuid();
    }
}
