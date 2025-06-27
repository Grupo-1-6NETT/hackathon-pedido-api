using Application.Exceptions;
using Application.Validators;
using MediatR;

namespace Application.Commands;
public class UpdatePedidoItemCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Quantidade { get; set; }

    public void Validate()
    {
        var validator = new UpdatePedidoItemValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}
