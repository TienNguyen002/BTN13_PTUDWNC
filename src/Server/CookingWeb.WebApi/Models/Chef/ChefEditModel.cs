using CookingWeb.Core.Entities;

namespace CookingWeb.WebApi.Models.Chef
{
    public class ChefEditModel
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
