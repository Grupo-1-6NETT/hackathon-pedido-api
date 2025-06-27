using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class UpdatePedidoItemValidator : AbstractValidator<UpdatePedidoItemCommand>
{
    public UpdatePedidoItemValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Um Id válido deve ser informado");
    }
}
