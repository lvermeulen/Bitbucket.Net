using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace Bitbucket.Net.Common
{
    public class DynamicMultipartFormDataContent : IEnumerable<HttpContent>
    {
        private readonly MultipartFormDataContent _multipartFormDataContent = new MultipartFormDataContent();

        public void Add(HttpContent value, string key)
        {
            _multipartFormDataContent.Add(value, key);
        }

        public void Add<T>(T t, HttpContent value, string key)
        {
            if (!EqualityComparer<T>.Default.Equals(t, default(T)))
            {
                _multipartFormDataContent.Add(value, key);
            }
        }

        public MultipartFormDataContent ToMultipartFormDataContent() => _multipartFormDataContent;

        public IEnumerator<HttpContent> GetEnumerator()
        {
            return _multipartFormDataContent.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
