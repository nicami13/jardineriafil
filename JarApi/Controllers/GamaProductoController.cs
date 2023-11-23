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
    public class GamaProductoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GamaProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GamaProductoDto>>> Get()
        {
            var gamasProductos = await _unitOfWork.GamaProductos.GetAllAsync();

            return _mapper.Map<List<GamaProductoDto>>(gamasProductos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GamaProductoDto>> Get(string id)
        {
            var gamaProducto = await _unitOfWork.GamaProductos.GetByIdSTring(id);
            if (gamaProducto == null)
                return NotFound();

            return _mapper.Map<GamaProductoDto>(gamaProducto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GamaProductoDto>> Post(GamaProductoDto gamaProductoDto)
        {
            var gamaProducto = _mapper.Map<GamaProducto>(gamaProductoDto);
            _unitOfWork.GamaProductos.Add(gamaProducto);
            await _unitOfWork.SaveAsync();

            if (gamaProducto == null)
            {
                return BadRequest();
            }

            gamaProductoDto.Gama = gamaProducto.Gama;

            return CreatedAtAction(nameof(Post), new { id = gamaProductoDto.Gama }, gamaProductoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GamaProductoDto>> Put(string id, [FromBody] GamaProductoDto gamaProductoDto)
        {
            if (gamaProductoDto == null)
                return NotFound();

            var gamaProducto = await _unitOfWork.GamaProductos.GetByIdSTring(id);
            if (gamaProducto == null)
                return NotFound();

            var updatedGamaProducto = _mapper.Map<GamaProducto>(gamaProductoDto);
            _unitOfWork.GamaProductos.Update(updatedGamaProducto);
            await _unitOfWork.SaveAsync();

            return gamaProductoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var gamaProducto = await _unitOfWork.GamaProductos.GetByIdSTring(id);
            if (gamaProducto == null)
                return NotFound();

            _unitOfWork.GamaProductos.Remove(gamaProducto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
