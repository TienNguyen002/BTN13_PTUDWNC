using CookingWeb.WebApi.Models.Chef;
using CookingWeb.WebApi.Models.Demand;
using CookingWeb.WebApi.Models.NumberOfSessions;
using CookingWeb.WebApi.Models.Price;

namespace CookingWeb.WebApi.Models.Course
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public bool Published { get; set; }
        public int RegisterCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DemandDto Demand { get; set; }
        public PriceDto Price { get; set; }
        public NumberOfSessionDto NumberOfSessions { get; set; }
        public ChefDto Chef { get; set; }
    }
}
