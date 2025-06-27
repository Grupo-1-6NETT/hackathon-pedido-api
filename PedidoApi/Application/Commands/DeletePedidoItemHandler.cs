using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class DeletePedidoItemHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<DeletePedidoItemCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:DeletePedidoItemQueueName"] ?? "deletepedidoitem";

    public Task<Guid> Handle(DeletePedidoItemCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var dto = new DeletePedidoItemDto
        {
            Id = request.Id,
        };

        rabbitMq.Publish(dto, _queue);

        return Task.FromResult(dto.Id);
    }
}
