using System.Collections;
using System.Collections.Generic;

namespace Bitbucket.Net.Common
{
    public class DynamicDictionary : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public void Add<T>(T t, string key, object value = null)
        {
            if (!EqualityComparer<T>.Default.Equals(t, default(T)))
            {
                _dictionary.Add(key, value ?? t);
            }
        }

        public IDictionary<string, object> ToDictionary() => _dictionary;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
