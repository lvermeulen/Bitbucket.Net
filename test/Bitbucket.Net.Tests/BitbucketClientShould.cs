using System.IO;
using Microsoft.Extensions.Configuration;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        private readonly BitbucketClient _client;

        public BitbucketClientShould()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _client = new BitbucketClient(configuration["url"], configuration["username"], configuration["password"]);
        }
    }
}
