using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class DeletePedidoItemValidator : AbstractValidator<DeletePedidoItemCommand>
{
    public DeletePedidoItemValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Um Id válido deve ser informado");
    }
}
