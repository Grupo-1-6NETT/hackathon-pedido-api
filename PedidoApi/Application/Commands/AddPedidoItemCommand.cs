using Application.Exceptions;
using Application.Validators;
using MediatR;

namespace Application.Commands;
public class AddPedidoItemCommand : IRequest<Guid>
{    
    public Guid ItemId { get; set; }
    public Guid PedidoId { get; set; }
    public int Quantidade { get; set; }

    public void Validate()
    {
        var validator = new AddPedidoItemValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}
