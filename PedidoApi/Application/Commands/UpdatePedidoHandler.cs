using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class UpdatePedidoHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<UpdatePedidoCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:UpdateItemQueueName"] ?? "updateitem";

    public Task<Guid> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var dto = new UpdatePedidoDto
        {
            Id = request.Id,
            Status = request.Status,
            Entrega = request.Entrega,
        };

        rabbitMq.Publish(dto, _queue);

        return Task.FromResult(dto.Id);
    }
}
