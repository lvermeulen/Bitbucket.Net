﻿using Bitbucket.Net.Core.Models.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class FileTypesConverter : JsonEnumConverter<FileTypes>
    {
        protected override string ConvertToString(FileTypes value)
        {
            return BitbucketHelpers.FileTypeToString(value);
        }
    }
}
