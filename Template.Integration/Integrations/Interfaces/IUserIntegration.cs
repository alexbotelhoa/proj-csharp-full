using Template.Core.Helpers;
using System.Threading.Tasks;

namespace Template.Integration.Integrations.Interfaces
{
    public interface IUserIntegration
    {
        Task<dynamic> CreateCall(string method, HeaderRequestHelper header, string endpoint, object query, object body);
    }
}
