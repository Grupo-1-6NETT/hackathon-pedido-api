using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class UpdatePedidoItemHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<UpdatePedidoItemCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:UpdatePedidoItemQueueName"] ?? "updatepedidoitem";

    public Task<Guid> Handle(UpdatePedidoItemCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var dto = new UpdatePedidoItemDto
        {
            Id = request.Id,
            Quantidade = request.Quantidade            
        };

        rabbitMq.Publish(dto, _queue);

        return Task.FromResult(dto.Id);
    }
}
