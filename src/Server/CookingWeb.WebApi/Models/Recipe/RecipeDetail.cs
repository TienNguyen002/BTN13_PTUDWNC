using CookingWeb.WebApi.Models.Author;
using CookingWeb.WebApi.Models.Course;

namespace CookingWeb.WebApi.Models.Recipe
{
    public class RecipeDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public string Ingredient { get; set; }
        public string Step { get; set; }
        public bool Published { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public AuthorDto Author { get; set; }
        public CourseDto Course { get; set; }
    }
}
