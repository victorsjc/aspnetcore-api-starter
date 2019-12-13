using System.Threading.Tasks;
using Web.Api.Core.Dto;
using System.Collections.Generic;

namespace Web.Api.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, List<string> userRoles);
    }
}
