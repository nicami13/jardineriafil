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
    public class EmpleadoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
        {
            var empleados = await _unitOfWork.Empleados.GetAllAsync();

            return _mapper.Map<List<EmpleadoDto>>(empleados);
        }
        [HttpGet("jefeCode7")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpNPPEDto>>> GetEmployeesWithJefeCode7()
        {
            try
            {
                var empleados = await _unitOfWork.Empleados.GetEmployedTO7();
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoDto>> Get(int id)
        {
            var empleado = await _unitOfWork.Empleados.GetByIdInt(id);
            if (empleado == null)
                return NotFound();

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>> Post(EmpleadoDto empleadoDto)
        {
            var empleado = _mapper.Map<Empleado>(empleadoDto);
            _unitOfWork.Empleados.Add(empleado);
            await _unitOfWork.SaveAsync();

            if (empleado == null)
            {
                return BadRequest();
            }

            empleadoDto.CodigoEmpleado = empleado.CodigoEmpleado;

            return CreatedAtAction(nameof(Post), new { id = empleadoDto.CodigoEmpleado }, empleadoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto empleadoDto)
        {
            if (empleadoDto == null)
                return NotFound();

            var empleado = await _unitOfWork.Empleados.GetByIdInt(id);
            if (empleado == null)
                return NotFound();

            var updatedEmpleado = _mapper.Map<Empleado>(empleadoDto);
            _unitOfWork.Empleados.Update(updatedEmpleado);
            await _unitOfWork.SaveAsync();

            return empleadoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _unitOfWork.Empleados.GetByIdInt(id);
            if (empleado == null)
                return NotFound();

            _unitOfWork.Empleados.Remove(empleado);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
