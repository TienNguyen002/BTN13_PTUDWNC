using CookingWeb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UrlSlug { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Notes { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
