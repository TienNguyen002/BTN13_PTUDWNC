using CookingWeb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Entities
{
    public class Chef : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime JoinedDate { get; set; }
        public string UrlSlug { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
