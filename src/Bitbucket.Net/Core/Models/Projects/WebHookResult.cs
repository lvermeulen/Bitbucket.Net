﻿using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class WebHookResult
    {
        public string Description { get; set; }
        [JsonConverter(typeof(WebHookOutcomesConverter))]
        public WebHookOutcomes Outcome { get; set; }
    }
}
