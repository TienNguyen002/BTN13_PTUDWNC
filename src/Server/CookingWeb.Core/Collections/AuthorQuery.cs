using CookingWeb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Collections
{
    public class AuthorQuery : IAuthorQuery
    {
        public string Keyword { get; set; } = "";
        public int Month { get; set; } = 0;
        public int Year { get; set; } = 0;
    }
}
