using System.Collections.Generic;

namespace Bitbucket.Net.Common.Models
{
    public class ErrorResponse
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
