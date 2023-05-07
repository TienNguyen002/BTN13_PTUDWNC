namespace CookingWeb.WebApi.Models.Course
{
    public class CourseEditModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public int DemandId { get; set; }
        public int PriceId { get; set; }
        public int NumberOfSessionsId { get; set; }
        public int ChefId { get; set; }
    }
}
