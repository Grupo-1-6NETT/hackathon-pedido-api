using Application.Commands;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardapioApi.Controllers;

//[Authorize]
[Route("[controller]")]
[ApiController]
public class PedidoItemController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Adiciona um PedidoItem na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "itemid": "00000000-0000-0000-0000-000000000000",
    ///     "pedidoid": "00000000-0000-0000-0000-000000000000",
    ///     "quantidade": "1"
    ///  }
    /// 
    /// </remarks>
    /// <param name="command">Comando com os dados do PedidoItem</param>
    /// <returns>O Id do PedidoItem adicionado</returns>
    /// <response code="201">PedidoItem adicionado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddPedidoItemCommand request)
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
    /// Atualiza um PedidoItem na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "id" : "00000000-0000-0000-0000-000000000000"    
    ///     "quantidade" : "2"
    ///  }
    /// 
    /// </remarks>
    /// <param name="command">Comando com os dados do PedidoItem</param>
    /// <returns>O Id do PedidoItem atualizado</returns>
    /// <response code="200">PedidoItem atualizado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="404">PedidoItem não encontrado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[Authorize]
    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] UpdatePedidoItemCommand request)
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
    /// Remove o PedidoItem na base de dados com o ID informado
    /// </summary>
    /// <param name="id">O ID do PedidoItem a ser removido</param>
    /// <returns>Resultado da operação de remoção</returns>
    /// <response code="200">PedidoItem removido com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="404">PedidoItem não encontrado</response>
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
            var command = new DeletePedidoItemCommand(id);
            await sender.Send(command);

            return Ok($"PedidoItem com {id} enviado para remoção.");
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
