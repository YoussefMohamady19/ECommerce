using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("CustomerId is required.");
            RuleFor(x => x.Items).NotNull().Must(list => list.Any()).WithMessage("Order must contain at least one item.");
            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
        }

        private class OrderItemValidator : AbstractValidator<OrderItemDto>
        {
            public OrderItemValidator()
            {
                RuleFor(x => x.ProductId).GreaterThan(0);
                RuleFor(x => x.Quantity).GreaterThan(0);
            }
        }
    }
}
