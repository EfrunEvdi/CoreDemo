using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori başlık kısmı boş geçilemez.");
            RuleFor(x => x.CategoryName).MinimumLength(5).WithMessage("Kategori başlık kısmı minimum 5 karekterden oluşmalıdır.");
            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Kategori başlık kısmı maksimum 50 karekterden oluşmalıdır.");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Kategori açıklama kısmı boş geçilemez.");
        }
    }
}
