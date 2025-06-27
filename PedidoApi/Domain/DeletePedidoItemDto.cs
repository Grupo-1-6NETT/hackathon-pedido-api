using MassTransit;

namespace Domain;

[MessageUrn("delete-pedidoitem-dto")]
[EntityName("delete-pedidoitem-dto")]
public class DeletePedidoItemDto
{
    public Guid Id { get; set; }
}
