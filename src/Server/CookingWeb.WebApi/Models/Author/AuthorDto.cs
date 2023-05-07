namespace CookingWeb.WebApi.Models.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UrlSlug { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
