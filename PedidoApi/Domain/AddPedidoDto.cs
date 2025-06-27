using MassTransit;

namespace Domain;

[MessageUrn("add-pedido-dto")]
[EntityName("add-pedido-dto")]
public class AddPedidoDto
{
    public Guid TransportId { get; set; }
    public string ClienteCpf { get; set; } = string.Empty;
    public string Entrega { get; set; } = string.Empty;
    public PedidoItemDto[] Items { get; set; }
}

[MessageUrn("pedido-item-dto")]
[EntityName("pedido-item-dto")]
public class PedidoItemDto
{
    public Guid ItemId { get; set; }
    public int Quantidade { get; set; }
}
