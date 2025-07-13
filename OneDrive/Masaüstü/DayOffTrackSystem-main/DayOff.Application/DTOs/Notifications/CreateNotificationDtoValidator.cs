using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Notifications
{
    public class CreateNotificationDtoValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationDtoValidator()
        {
            // UserId opsiyonel girildiyse de, varsa geçerli bir değer olmalı
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .When(x => x.UserId.HasValue)
                .WithMessage("Kullanıcı ID geçersiz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Bildirim başlığı boş olamaz.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj içeriği boş olamaz.")
                .MaximumLength(500).WithMessage("Mesaj en fazla 500 karakter olabilir.");
        }
    }
}
