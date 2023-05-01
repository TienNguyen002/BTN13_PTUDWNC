using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

namespace CookingWeb.WebApi.Models.Recipe
{
    public class RecipeFilterModel : PagingModel
    {
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; }

        [DisplayName("Tác giả")]
        public int? AuthorId { get; set; }

        [DisplayName("Khóa học")]
        public int? CourseId { get; set; }

        [DisplayName("Tháng")]
        public int? CreateMonth { get; set; }

        [DisplayName("Năm")]
        public int? CreateYear { get; set; }

        public IEnumerable<SelectListItem> AuthorList { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> MonthList { get; set; }

        public RecipeFilterModel()
        {
            CultureInfo.CurrentCulture = new CultureInfo("vi-VN");
            MonthList = Enumerable.Range(1, 12)
            .Select(m => new SelectListItem()
            {
                Value = m.ToString(),
                Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
            })
            .ToList();
        }
    }
}
