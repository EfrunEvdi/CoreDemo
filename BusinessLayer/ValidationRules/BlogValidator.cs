using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog başlık kısmı boş geçilemez.");
            RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("Blog başlık kısmı minimum 5 karekterden oluşmalıdır.");
            RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("Blog başlık kısmı maksimum 150 karekterden oluşmalıdır.");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("Blog içerik kısmı boş geçilemez.");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Blog görsel kısmı boş geçilemez.");
            RuleFor(x => x.BlogThumbnailImage).NotEmpty().WithMessage("Blog ana görsel kısmı boş geçilemez.");
        }
    }
}
