using CookingWeb.WebApi.Models.Post;
using FluentValidation;

namespace CookingWeb.WebApi.Validations
{
    public class PostValidator : AbstractValidator<PostEditModel>
    {
        public PostValidator() 
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Tiêu đề không được để trống")
                .MaximumLength(1000)
                .WithMessage("Tiêu đề chỉ tối đa 1000 ký tự");

            RuleFor(p => p.ShortDescription)
                .NotEmpty()
                .WithMessage("Giới thiệu không được để trống");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("Nội dung không được để trống");

            RuleFor(p => p.Metadata)
                .NotEmpty()
                .WithMessage("Metadata không được để trống");

            RuleFor(p => p.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống");

            RuleFor(p => p.AuthorId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn tác giả cho bài viết");

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn chủ đề cho bài viết");
        }
    }
}
