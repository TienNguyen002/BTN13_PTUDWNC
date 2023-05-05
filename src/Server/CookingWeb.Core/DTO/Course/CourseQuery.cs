using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.DTO.Course
{
    public class CourseQuery
    {
        public string Keyword { get; set; }
        public int? DemandId { get; set; }
        public string DemandName { get; set; }
        public string DemandSlug { get; set; }
        public int? PriceId { get; set; }
        public string PriceName { get; set; }
        public string PriceSlug { get; set; }
        public int? NumberOfSessionsId { get; set; }
        public string NumberOfSessionsName { get; set; }
        public string NumberOfSessionsSlug { get; set; }
        public int? ChefId { get; set; }
        public string ChefName { get; set; }
        public string ChefSlug { get; set; }
        public int? CreateMonth { get; set; }
        public int? CreateYear { get; set; }
        public bool PublishedOnly { get; set; }
        public bool NotPublished { get; set; }
        public bool Status { get; set; }
    }
}
