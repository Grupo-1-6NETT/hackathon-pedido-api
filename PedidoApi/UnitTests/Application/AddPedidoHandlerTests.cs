using Application.Commands;
using Application.Exceptions;
using Application.Services;
using Domain;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Application;
public class AddPedidoHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AddPedidoHandler _sut;

    public AddPedidoHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _configurationMock = new Mock<IConfiguration>();
        _sut = new(_rabbitMqServiceMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Handle_InformadosDadosValidos_DeveRetornarOk()
    {
        var command = new AddPedidoCommand
        {
            ClienteCpf = "00000000000",
            Entrega = "Balcao"
        };

        var result = await _sut.Handle(command, CancellationToken.None);

        _rabbitMqServiceMock.Verify(x => x.Publish(It.IsAny<AddPedidoDto>(), It.IsAny<string>()), Times.Once);

        _rabbitMqServiceMock.Invocations.Clear();
    }

    [Fact]
    public async Task Handle_InformadosDadosInvalidos_ValidationException()
    {
        var command = new AddPedidoCommand
        {
            ClienteCpf = ""
        };

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(command, CancellationToken.None));
    }
}
