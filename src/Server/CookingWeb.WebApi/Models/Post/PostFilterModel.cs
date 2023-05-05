using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

namespace CookingWeb.WebApi.Models.Post
{
    public class PostFilterModel : PagingModel
    {
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; }

        [DisplayName("Tác giả")]
        public int? AuthorId { get; set; }

        [DisplayName("Chủ đề")]
        public int? CategoryId { get; set; }

        [DisplayName("Tháng")]
        public int? CreateMonth { get; set; }

        [DisplayName("Năm")]
        public int? CreateYear { get; set; }

        [DisplayName("Trạng thái")]
        public bool? PublishedOnly { get; set; }
        public bool? NotPublished { get; set; }

        public IEnumerable<SelectListItem> AuthorList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> MonthList { get; set; }

        public PostFilterModel()
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
