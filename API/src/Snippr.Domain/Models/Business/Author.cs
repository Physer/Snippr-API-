﻿using System.Collections.Generic;

namespace Snippr.Domain.Models.Business
{
    public class Author
    {
        public string Username { get; set; } = Constants.UserProperties.Anonymous;
        public IEnumerable<CodeSnippet> OwnedCodeSnippets { get; set; }
    }
}
