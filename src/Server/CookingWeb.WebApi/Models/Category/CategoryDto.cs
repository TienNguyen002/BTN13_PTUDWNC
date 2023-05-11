using CookingWeb.WebApi.Models.Others;

namespace CookingWeb.WebApi.Models.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public TopicDto Topic { get; set; }
    }
}
