﻿using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidatior : AbstractValidator<Writer>
    {
        public WriterValidatior() //yapıcı metot
        {
            //Fluent Validation kuralları

            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adını boş geçemezsiniz"); //boş olamaz
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar soyadını boş geçemezsiniz");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın");
            RuleFor(x => x.WriterSurname).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayın");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkımda kısmını boş geçemezsiniz");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan kısmını boş geçemezsiniz");
            RuleFor(x => x.WriterAbout).Must(x=>x!=null && x.ToUpper().Contains("A")).WithMessage("Hakkımda kısmında en az bir a harfi içermelidir");

        }
    }
}
