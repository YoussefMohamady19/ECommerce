using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Valid email is required.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^01[0-9]{9}$")
                .WithMessage("Phone number must be a valid Egyptian mobile (e.g. 010xxxxxxxx).");
        }
    }
}
