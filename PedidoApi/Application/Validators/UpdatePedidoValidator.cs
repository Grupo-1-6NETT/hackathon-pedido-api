using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class UpdatePedidoValidator : AbstractValidator<UpdatePedidoCommand>
{
    public UpdatePedidoValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Um Id válido deve ser informado");
    }
}
