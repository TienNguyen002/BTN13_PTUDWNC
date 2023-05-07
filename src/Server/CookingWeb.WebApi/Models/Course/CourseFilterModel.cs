using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

namespace CookingWeb.WebApi.Models.Course
{
    public class CourseFilterModel : PagingModel
    {
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; }

        [DisplayName("Nhu cầu")]
        public int? DemandId { get; set; }

        [DisplayName("Giá")]
        public int? PriceId { get; set; }

        [DisplayName("Số buổi học")]
        public int? NumberOfSessionsId { get; set; }

        [DisplayName("Đầu bếp")]
        public int? ChefId { get; set; }

        [DisplayName("Tháng")]
        public int? CreateMonth { get; set; }

        [DisplayName("Năm")]
        public int? CreateYear { get; set; }

        [DisplayName("Trạng thái")]
        public bool? PublishedOnly { get; set; }
        public bool? NotPublished { get; set; }

        public IEnumerable<SelectListItem> DemandList { get; set; }
        public IEnumerable<SelectListItem> PriceList { get; set; }
        public IEnumerable<SelectListItem> NumberOfSessionsList { get; set; }
        public IEnumerable<SelectListItem> ChefList { get; set; }
        public IEnumerable<SelectListItem> MonthList { get; set; }

        public CourseFilterModel()
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
