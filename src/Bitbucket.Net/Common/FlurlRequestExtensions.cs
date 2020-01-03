using System;
using Flurl.Http;

namespace Bitbucket.Net.Common
{
    public static class FlurlRequestExtensions
    {
        public static IFlurlRequest WithAuthentication(this IFlurlRequest request, Func<string> getToken, string userName, string password)
        {
            if (getToken != null)
            {
                string token = getToken();
                return request.WithOAuthBearerToken(token);
            }

            return request.WithBasicAuth(userName, password);
        }
    }
}
