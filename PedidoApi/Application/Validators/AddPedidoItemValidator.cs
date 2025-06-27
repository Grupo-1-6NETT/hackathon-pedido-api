using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class AddPedidoItemValidator : AbstractValidator<AddPedidoItemCommand>
{
    public AddPedidoItemValidator()
    {
        RuleFor(i => i.ItemId)
            .NotEmpty().WithMessage("{PropertyName} é obrigatória");

        RuleFor(i => i.PedidoId)
            .NotEmpty().WithMessage("{PropertyName} é obrigatória");

        RuleFor(i => i.Quantidade)
            .GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero");            
    }
}
