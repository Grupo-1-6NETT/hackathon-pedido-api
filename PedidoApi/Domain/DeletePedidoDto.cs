using MassTransit;

namespace Domain;

[MessageUrn("delete-pedido-dto")]
[EntityName("delete-pedido-dto")]
public class DeletePedidoDto
{
    public Guid Id { get; set; }
}
