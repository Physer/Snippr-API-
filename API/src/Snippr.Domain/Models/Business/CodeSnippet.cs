﻿using System;
using System.Collections.Generic;

namespace Snippr.Domain.Models.Business
{
    public class CodeSnippet
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Upvotes { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Category> Categories { get; set; } 
    }
}
