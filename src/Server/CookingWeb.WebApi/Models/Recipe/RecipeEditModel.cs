namespace CookingWeb.WebApi.Models.Recipe
{
    public class RecipeEditModel
    {
        public string Title { get; set; }
        public string ShortDesciption { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public string UrlSlug { get; set; }
        public string Ingredient { get; set; }
        public string Step { get; set; }
        public bool Published { get; set; }
        public int AuthorId { get; set; }
        public int CourseId { get; set; }
    }
}
