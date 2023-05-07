using CookingWeb.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Collections
{
    public class CategoryQuery : ICategoryQuery
    {
        public string Keyword { get; set; } = "";
        public bool ShowOnMenu { get; set; } = false;
    }

}
