using System.Collections.Generic;

namespace Bitbucket.Net.Models.Common
{
    public class ErrorResponse
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
