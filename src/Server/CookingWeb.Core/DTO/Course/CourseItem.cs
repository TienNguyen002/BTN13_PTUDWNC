using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.DTO.Course
{
    public class CourseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UrlSlug { get; set; }
    }
}
