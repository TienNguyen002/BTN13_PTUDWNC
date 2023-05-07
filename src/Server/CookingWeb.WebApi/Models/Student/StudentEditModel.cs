using CookingWeb.Core.Entities;

namespace CookingWeb.WebApi.Models.Student
{
    public class StudentEditModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UrlSlug { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Notes { get; set; }
    }
}
