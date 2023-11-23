
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
    public class JefeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JefeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<JefeDto>>> Get()
        {
            var jefes = await _unitOfWork.Jefes.GetAllAsync();

            return _mapper.Map<List<JefeDto>>(jefes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JefeDto>> Get(int id)
        {
            var jefe = await _unitOfWork.Jefes.GetByIdInt(id);
            if (jefe == null)
                return NotFound();

            return _mapper.Map<JefeDto>(jefe);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JefeDto>> Post(JefeDto jefeDto)
        {
            var jefe = _mapper.Map<Jefe>(jefeDto);
            _unitOfWork.Jefes.Add(jefe);
            await _unitOfWork.SaveAsync();

            if (jefe == null)
            {
                return BadRequest();
            }

            jefeDto.CodigoJefe = jefe.CodigoJefe;  // Ajusta según la propiedad de identificación de tu entidad Jefe

            return CreatedAtAction(nameof(Post), new { id = jefeDto.CodigoJefe }, jefeDto);
        }

        // ... Código previo ...

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JefeDto>> Put(int id, [FromBody] JefeDto jefeDto)
        {
            if (jefeDto == null)
                return NotFound();

            var jefe = await _unitOfWork.Jefes.GetByIdInt(id);
            if (jefe == null)
                return NotFound();

  
            jefe.Nombre = jefeDto.Nombre;

            _unitOfWork.Jefes.Update(jefe);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<JefeDto>(jefe);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var jefe = await _unitOfWork.Jefes.GetByIdInt(id);
            if (jefe == null)
                return NotFound();

            _unitOfWork.Jefes.Remove(jefe);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }


    }
}
