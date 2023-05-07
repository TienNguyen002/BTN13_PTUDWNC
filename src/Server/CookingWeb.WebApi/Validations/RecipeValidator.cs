using CookingWeb.WebApi.Models.Recipe;
using FluentValidation;

namespace CookingWeb.WebApi.Validations
{
    public class RecipeValidator : AbstractValidator<RecipeEditModel>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty()
                .WithMessage("Tiêu đề không được để trống")
                .MaximumLength(1000)
                .WithMessage("Tiêu đề chỉ tối đa 1000 ký tự");

            RuleFor(r => r.ShortDescription)
                .NotEmpty()
                .WithMessage("Giới thiệu không được để trống");

            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Nội dung không được để trống");

            RuleFor(r => r.Metadata)
                .NotEmpty()
                .WithMessage("Metadata không được để trống");

            RuleFor(r => r.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống");

            RuleFor(r => r.Ingredient)
                .NotEmpty()
                .WithMessage("Nguyên liệu không được để trống");

            RuleFor(r => r.Step)
                .NotEmpty()
                .WithMessage("Các bước tiến hành không được để trống");

            RuleFor(r => r.AuthorId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn tác giả cho công thức");

            RuleFor(r => r.CourseId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn khóa học cho công thức");
        }
    }
}
