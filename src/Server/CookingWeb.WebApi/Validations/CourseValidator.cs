using CookingWeb.WebApi.Models.Course;
using FluentValidation;

namespace CookingWeb.WebApi.Validations
{
    public class CourseValidator : AbstractValidator<CourseEditModel>
    {
        public CourseValidator() 
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("Tiêu đề không được để trống")
                .MaximumLength(1000)
                .WithMessage("Tiêu đề chỉ tối đa 1000 ký tự");

            RuleFor(c => c.ShortDescription)
                .NotEmpty()
                .WithMessage("Giới thiệu không được để trống");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Nội dung không được để trống");

            RuleFor(c => c.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống");

            RuleFor(c => c.DemandId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn nhu cầu của khóa học");

            RuleFor(c => c.PriceId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn giá của khóa học");

            RuleFor(c => c.NumberOfSessionsId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn số buổi của khóa học");
        }
    }
}
