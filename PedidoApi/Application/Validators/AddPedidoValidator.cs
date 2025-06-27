using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class AddPedidoValidator : AbstractValidator<AddPedidoCommand>
{
    public AddPedidoValidator()
    {
        RuleFor(i => i.Entrega)
            .NotEmpty().WithMessage("{PropertyName} é obrigatória");
        
        RuleFor(i => i.ClienteCpf)
            .NotEmpty().WithMessage("{PropertyName} é obrigatório")
            .Length(11).WithMessage("{PropertyName} deve ter 11 caracteres");
    }
}
