using Application.Commands;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardapioApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class PedidoController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Adiciona um Pedido na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "clientecpf": "00000000000",
    ///     "entrega": "Balcao"
    ///  }
    ///  
    /// Possíveis valores para entrega: [Balcao, Drivethru,Delivery]
    /// 
    /// </remarks>
    /// <param name="command">Comando com os dados do Pedido</param>
    /// <returns>O Id do Pedido adicionado</returns>
    /// <response code="201">Pedido adicionado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddPedidoCommand request)
    {
		try
		{
			var id = await sender.Send(request);
			return Created("", id);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new { ex.Errors});
		}
		catch (Exception)
		{
			return StatusCode(500, new { Message = $"Ocorreu um erro interno no servidor." });
        }
    }

    /// <summary>
    /// Atualiza um Pedido na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "id" : "00000000-0000-0000-0000-000000000000"
    ///     "status" : "Aceito",
    ///     "entrega": "Balcao"
    ///  }
    ///  
    /// Possíveis valores para status: [Pendente, Aceito, Rejeitado, Cancelado]
    /// Possíveis valores para entrega: [Balcao, Drivethru,Delivery]
    /// 
    /// </remarks>
    /// <param name="command">Comando com os dados do Pedido</param>
    /// <returns>O Id do Pedido atualizado</returns>
    /// <response code="200">Pedido atualizado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="404">Pedido não encontrado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] UpdatePedidoCommand request)
    {
        try
        {
            var id = await sender.Send(request);
            return Ok(id);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { ex.Errors });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = $"Ocorreu um erro interno no servidor." });
        }
    }

    /// <summary>
    /// Remove o Pedido na base de dados com o ID informado
    /// </summary>
    /// <param name="id">O ID do Pedido a ser removido</param>
    /// <returns>Resultado da operação de remoção</returns>
    /// <response code="200">Pedido removido com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="404">Pedido não encontrado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeletePedidoCommand(id);
            await sender.Send(command);

            return Ok($"Pedido com {id} enviado para remoção.");
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { ex.Errors });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = $"Ocorreu um erro interno no servidor." });
        }
    }
}
