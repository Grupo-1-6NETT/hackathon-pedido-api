using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class DeletePedidoHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<DeletePedidoCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:DeletePedidoQueueName"] ?? "deletepedido";

    public Task<Guid> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var dto = new DeletePedidoDto
        {
            Id = request.Id,
        };

        rabbitMq.Publish(dto, _queue);

        return Task.FromResult(dto.Id);
    }
}
