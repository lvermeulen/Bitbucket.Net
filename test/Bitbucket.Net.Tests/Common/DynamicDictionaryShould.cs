using Bitbucket.Net.Common;
using Xunit;

namespace Bitbucket.Net.Tests.Common
{
    public class DynamicDictionaryShould
    {
        private readonly DynamicDictionary _dictionary = new DynamicDictionary();

        [Fact]
        public void AddNotNullItems()
        {
            const string VALUE = "Hello";
            const string NAME = "Joe";

            _dictionary.Add(VALUE, nameof(VALUE));
            Assert.Contains(_dictionary.ToDictionary(), kvp => kvp.Key == nameof(VALUE) && (string)kvp.Value == "Hello");

            _dictionary.Add(NAME, nameof(NAME), NAME);
            Assert.Contains(_dictionary.ToDictionary(), kvp => kvp.Key == nameof(NAME) && (string)kvp.Value == "Joe");
        }

        [Fact]
        public void NotAddNullItems()
        {
            const string NAME = null;

            _dictionary.Add(NAME, nameof(NAME));
            Assert.Empty(_dictionary.ToDictionary());
        }
    }
}
