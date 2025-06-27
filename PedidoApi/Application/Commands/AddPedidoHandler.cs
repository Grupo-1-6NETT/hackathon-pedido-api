using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class AddPedidoHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<AddPedidoCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:AddPedidoQueueName"] ?? "addpedido";

    public Task<Guid> Handle(AddPedidoCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var dto = new AddPedidoDto
        {
            TransportId = Guid.NewGuid(),
            ClienteCpf = request.ClienteCpf,
            Entrega = request.Entrega,            
        };

        if (request.Items != null && request.Items.Any())
            dto.Items = request.Items
                .Select(x => new PedidoItemDto
                    {
                        ItemId = x.ItemId,
                        Quantidade = x.Quantidade,
                    })
                .ToArray();

        rabbitMq.Publish(dto, _queue);

        return Task.FromResult(dto.TransportId);
    }
}
