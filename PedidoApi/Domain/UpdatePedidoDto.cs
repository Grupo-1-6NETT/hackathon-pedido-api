using MassTransit;

namespace Domain;

[MessageUrn("update-pedido-dto")]
[EntityName("update-pedido-dto")]
public class UpdatePedidoDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public string Entrega { get; set; }    
}
