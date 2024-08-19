using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Commands.Update
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommandHandlerRequest>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("El Id de la orden debe ser mayor a 0.");

            RuleFor(x => x.StatusId)
                .Must(status => Enum.IsDefined(typeof(OrderStatusesEnum), status))
                .WithMessage("Id Invalido.");
        }
    }
}
