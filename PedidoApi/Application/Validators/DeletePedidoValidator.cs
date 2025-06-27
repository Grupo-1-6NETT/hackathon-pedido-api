using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class DeletePedidoValidator : AbstractValidator<DeletePedidoCommand>
{
    public DeletePedidoValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Um Id válido deve ser informado");
    }
}
