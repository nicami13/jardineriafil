using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using JarApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.entities;


namespace JarApi.Controllers
{
    public class OficinaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
        {
            var oficinas = await _unitOfWork.Oficinas.GetAllAsync();

            return _mapper.Map<List<OficinaDto>>(oficinas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OficinaDto>> Post(OficinaDto oficinaDto)
        {
            var oficina = _mapper.Map<Oficina>(oficinaDto);
            _unitOfWork.Oficinas.Add(oficina);
            await _unitOfWork.SaveAsync();

            if (oficina == null)
            {
                return BadRequest();
            }

            oficinaDto.CodigoOficina = oficina.CodigoOficina;

            return CreatedAtAction(nameof(Post), new { id = oficinaDto.CodigoOficina }, oficinaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OficinaDto>> Put(string id, [FromBody] OficinaDto oficinaDto)
        {
            if (oficinaDto == null)
                return NotFound();

            var oficina = await _unitOfWork.Oficinas.GetByIdSTring(id);
            if (oficina == null)
                return NotFound();

            var updatedOficina = _mapper.Map<Oficina>(oficinaDto);
            _unitOfWork.Oficinas.Update(updatedOficina);
            await _unitOfWork.SaveAsync();

            return oficinaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var oficina = await _unitOfWork.Oficinas.GetByIdSTring(id);
            if (oficina == null)
                return NotFound();

            _unitOfWork.Oficinas.Remove(oficina);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
