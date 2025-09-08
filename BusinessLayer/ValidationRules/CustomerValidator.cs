
    using EntityLayer.Concrete;
    using FluentValidation;

    namespace BusinessLayer.ValidationRules
    {
        public class CustomerValidator : AbstractValidator<Customer>
        {
            public CustomerValidator()
            {
                RuleFor(x => x.CariKodu).NotEmpty().WithMessage("Cari kodu boş geçilemez.");
                RuleFor(x => x.CariAdi).NotEmpty().WithMessage("Cari adı boş geçilemez.");
                RuleFor(x => x.CariTipi).NotEmpty().WithMessage("Cari tipi boş geçilemez.");
                RuleFor(x => x.VergiDairesi).NotEmpty().WithMessage("Vergi dairesi boş geçilemez.");
                RuleFor(x => x.VergiNo).NotEmpty().WithMessage("Vergi numarası boş geçilemez.");
                RuleFor(x => x.TCKimlikNo).NotEmpty().WithMessage("T.C. kimlik numarası boş geçilemez.");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilemez.")
                                     .EmailAddress().WithMessage("Geçersiz email formatı.");
                RuleFor(x => x.Telefon).NotEmpty().WithMessage("Telefon boş geçilemez.");
            }
        }
    }
