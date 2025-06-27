using Application.Exceptions;
using Application.Validators;
using MediatR;

namespace Application.Commands;
public class AddPedidoCommand : IRequest<Guid>
{    
    public string Entrega { get; set; } = string.Empty;
    public string ClienteCpf { get; set; } = string.Empty;    
    public PedidoItem[] Items { get; set; }

    public void Validate()
    {
        var validator = new AddPedidoValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}

public class PedidoItem
{
    public Guid ItemId { get; set; }
    public int Quantidade { get; set; }
}
