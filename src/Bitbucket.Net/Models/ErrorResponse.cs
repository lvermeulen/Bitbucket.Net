using System.Collections.Generic;

namespace Bitbucket.Net.Models
{
    public class ErrorResponse
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
