using CardapioApi.Controllers;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Commands;

namespace UnitTests.Api;

public class PedidoControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly PedidoController _sut;

    public PedidoControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new(_senderMock.Object);

    }

    #region Post
    [Fact]
    public async Task Post_InformadosDadosValidos_DeveRetornarCreatedResult()
    {
        var guid = Guid.Empty;

        var command = new AddPedidoCommand
        {
            ClienteCpf = "00000000000",
            Entrega = "Balcao"
        };
        _senderMock.Setup(m => m.Send(It.IsAny<AddPedidoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(guid);

        var result = await _sut.Post(command);
        var createdResult = Assert.IsType<CreatedResult>(result);        

        Assert.Equal(guid, createdResult.Value);
    }

    [Fact]
    public async Task Post_DadosInvalidos_DeveRetornarBadRequest()
    {
        var command = new AddPedidoCommand
        {
            ClienteCpf = ""
        };

        _senderMock
            .Setup(m => m.Send(It.IsAny<AddPedidoCommand>(), It.IsAny<CancellationToken>()))
            .Throws(new ValidationException(new List<string>()));

        var result = await _sut.Post(command);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    #endregion Post

    #region Patch
    [Fact]
    public async Task Patch_InformadosDadosValidos_DeveRetornarOk()
    {
        var guid = Guid.NewGuid();

        var command = new UpdatePedidoCommand
        {
            Id = guid,
            Entrega = "Balcao"
        };
        _senderMock.Setup(m => m.Send(It.IsAny<UpdatePedidoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(guid);

        var result = await _sut.Patch(command);
        var okResult = Assert.IsType<OkObjectResult>(result);        

        Assert.Equal(guid, okResult.Value);
    }

    [Fact]
    public async Task Patch_DadosInvalidos_DeveRetornarBadRequest()
    {
        var guid = Guid.Empty;
        var command = new UpdatePedidoCommand
        {
            Id = guid            
        };

        _senderMock
            .Setup(m => m.Send(It.IsAny<UpdatePedidoCommand>(), It.IsAny<CancellationToken>()))
            .Throws(new ValidationException(new List<string>()));

        var result = await _sut.Patch(command);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    #endregion Patch
}