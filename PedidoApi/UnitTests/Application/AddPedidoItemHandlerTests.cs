using Application.Commands;
using Application.Exceptions;
using Application.Services;
using Domain;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Application;
public class AddPedidoItemHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AddPedidoItemHandler _sut;

    public AddPedidoItemHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _configurationMock = new Mock<IConfiguration>();
        _sut = new(_rabbitMqServiceMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Handle_InformadosDadosValidos_DeveRetornarOk()
    {
        var command = new AddPedidoItemCommand
        {
            ItemId = Guid.NewGuid(),
            PedidoId = Guid.NewGuid(),
            Quantidade = 1
        };

        var result = await _sut.Handle(command, CancellationToken.None);

        _rabbitMqServiceMock.Verify(x => x.Publish(It.IsAny<AddPedidoItemDto>(), It.IsAny<string>()), Times.Once);

        _rabbitMqServiceMock.Invocations.Clear();
    }

    [Fact]
    public async Task Handle_InformadosDadosInvalidos_ValidationException()
    {
        var command = new AddPedidoItemCommand
        {
            ItemId = Guid.Empty,
            PedidoId = Guid.Empty,
            Quantidade = 0
        };

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(command, CancellationToken.None));
    }
}
