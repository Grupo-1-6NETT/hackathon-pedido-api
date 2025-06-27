using Application.Commands;
using Application.Exceptions;
using Application.Services;
using Domain;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Application;
public class UpdatePedidoHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly UpdatePedidoHandler _sut;

    public UpdatePedidoHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _configurationMock = new Mock<IConfiguration>();
        _sut = new(_rabbitMqServiceMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Handle_InformadosDadosValidos_DeveRetornarOk()
    {
        var guid = Guid.NewGuid();
        var command = new UpdatePedidoCommand
        {
            Id = guid,
            Status = "Aceito"
        };

        var result = await _sut.Handle(command, CancellationToken.None);

        _rabbitMqServiceMock.Verify(x => x.Publish(It.IsAny<UpdatePedidoDto>(), It.IsAny<string>()), Times.Once);

        _rabbitMqServiceMock.Invocations.Clear();
    }

    [Fact]
    public async Task Handle_InformadosDadosInvalidos_ValidationException()
    {
        var command = new UpdatePedidoCommand
        {
            Status = "error",
        };

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(command, CancellationToken.None));
    }
}
