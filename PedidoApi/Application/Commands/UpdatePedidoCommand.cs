using Application.Exceptions;
using Application.Validators;
using MediatR;

namespace Application.Commands;
public class UpdatePedidoCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public string Entrega { get; set; }

    public void Validate()
    {
        var validator = new UpdatePedidoValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}
