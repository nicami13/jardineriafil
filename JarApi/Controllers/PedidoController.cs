using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using JarApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.entities;
using ApiApolo.Controllers;

namespace JarApi.Controllers
{
    public class PedidoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
        {
            var pedidos = await _unitOfWork.Pedidos.GetAllAsync();

            return _mapper.Map<List<PedidoDto>>(pedidos);
        }
        [HttpGet("pedidosNoEntregadosATiempo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPedidosNoEntregadosATiempo()
        {
            try
            {
                var pedidos = await _unitOfWork.Pedidos.GetPedidosNoEntregadosATiempo();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                // Log y manejo de errores
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpGet("PedidosFechaEntregaDosDiasAntes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPedidosFechaEntregaDosDiasAntes()
        {
            try
            {
                var pedidos = await _unitOfWork.Pedidos.GetPedidosFechaEntregaDosDiasAntes();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet("Pagos2008Paypal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPagos2008Paypal()
        {
            try
            {
                var pagos = await _unitOfWork.Pedidos.GetPagos2008Paypal();
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet("PedidosRechazados2009")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPedidosRechazados2009()
        {
            try
            {
                var pedidos = await _unitOfWork.Pedidos.GetPedidosRechazados2009();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet("PedidosEntregados2009")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPedidosEntregados2009()
        {
            try
            {
                var pedidos = await _unitOfWork.Pedidos.GetPedidosEntregados2009();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoDto>> Get(int id)
        {
            var pedido = await _unitOfWork.Pedidos.GetByIdInt(id);
            if (pedido == null)
                return NotFound();

            return _mapper.Map<PedidoDto>(pedido);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoDto>> Post(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            _unitOfWork.Pedidos.Add(pedido);
            await _unitOfWork.SaveAsync();

            if (pedido == null)
            {
                return BadRequest();
            }

            pedidoDto.CodigoPedido = pedido.CodigoPedido;

            return CreatedAtAction(nameof(Post), new { id = pedidoDto.CodigoPedido }, pedidoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto pedidoDto)
        {
            if (pedidoDto == null)
                return NotFound();

            var pedido = await _unitOfWork.Pedidos.GetByIdInt(id);
            if (pedido == null)
                return NotFound();

            var updatedPedido = _mapper.Map<Pedido>(pedidoDto);
            _unitOfWork.Pedidos.Update(updatedPedido);
            await _unitOfWork.SaveAsync();

            return pedidoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _unitOfWork.Pedidos.GetByIdInt(id);
            if (pedido == null)
                return NotFound();

            _unitOfWork.Pedidos.Remove(pedido);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
