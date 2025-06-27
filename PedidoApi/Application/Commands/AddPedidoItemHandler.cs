using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class AddPedidoItemHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<AddPedidoItemCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:AddPedidoItemQueueName"] ?? "addpedidoitem";

    public Task<Guid> Handle(AddPedidoItemCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var dto = new AddPedidoItemDto
        {
            ItemId = request.ItemId,
            PedidoId = request.PedidoId,
            Quantidade = request.Quantidade,
        };

        rabbitMq.Publish(dto, _queue);

        return Task.FromResult(dto.TransportId);
    }
}
