using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar isim soyisim kısmı boş geçilemez.");
            RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Yazar isim soyisim kısmı maksimum 50 karakter olmalıdır.");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Yazar mail adresi kısmı boş geçilemez.");
            //RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre kısmı boş geçilemez.");
            RuleFor(x => x.WriterConfirmPassword).NotEmpty().WithMessage("Şifre tekrar kısmı boş geçilemez.");

            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre kısmı boş geçilemez.")
                            .MinimumLength(6).WithMessage("Şifreniz en az 6 karakter oluşmalıdır.")
                            .MaximumLength(9).WithMessage("Şifreniz en fazla 9 karakterden oluşmalıdır.")
                            .Matches(@"[A-Z]+").WithMessage("Şifrenizde en az bir büyük harf olmalıdır.")
                            .Matches(@"[a-z]+").WithMessage("Şifrede en az bir küçük harf olmalıdır.")
                            .Matches(@"[0-9]+").WithMessage("Şifrede en az bir rakam olmalıdır");
        }
    }
}
