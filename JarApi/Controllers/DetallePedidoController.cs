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
    public class DetallePedidoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetallePedidoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
        {
            var detallesPedidos = await _unitOfWork.DetallePedidos.GetAllAsync();

            return _mapper.Map<List<DetallePedidoDto>>(detallesPedidos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetallePedidoDto>> Get(int id)
        {
            var detallePedido = await _unitOfWork.DetallePedidos.GetByIdInt(id);
            if (detallePedido == null)
                return NotFound();

            return _mapper.Map<DetallePedidoDto>(detallePedido);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetallePedidoDto>> Post(DetallePedidoDto detallePedidoDto)
        {
            var detallePedido = _mapper.Map<DetallePedido>(detallePedidoDto);
            _unitOfWork.DetallePedidos.Add(detallePedido);
            await _unitOfWork.SaveAsync();

            if (detallePedido == null)
            {
                return BadRequest();
            }

            detallePedidoDto.CodigoPedido = detallePedido.CodigoPedido;
            detallePedidoDto.CodigoProducto = detallePedido.CodigoProducto;

            return CreatedAtAction(nameof(Post), new { id = detallePedidoDto.CodigoPedido, codigoProducto = detallePedidoDto.CodigoProducto }, detallePedidoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody] DetallePedidoDto detallePedidoDto)
        {
            if (detallePedidoDto == null)
                return NotFound();

            var detallePedido = await _unitOfWork.DetallePedidos.GetByIdInt(id);
            if (detallePedido == null)
                return NotFound();

            var updatedDetallePedido = _mapper.Map<DetallePedido>(detallePedidoDto);
            _unitOfWork.DetallePedidos.Update(updatedDetallePedido);
            await _unitOfWork.SaveAsync();

            return detallePedidoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var detallePedido = await _unitOfWork.DetallePedidos.GetByIdInt(id);
            if (detallePedido == null)
                return NotFound();

            _unitOfWork.DetallePedidos.Remove(detallePedido);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
