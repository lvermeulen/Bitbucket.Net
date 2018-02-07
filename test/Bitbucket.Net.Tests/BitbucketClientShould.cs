namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        private readonly BitbucketClient _client;

        public BitbucketClientShould()
        {
            _client = new BitbucketClient("", "", "");
        }
    }
}
