using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractice.Models;

namespace webApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public equiposController(equiposContext equiposContexto)
        {
            _equiposContext = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get() 
        {
            List<equipos> listadoEquipo = (from e in _equiposContext.equipos select e).ToList();
            if(listadoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContext.equipos
                               where e.id_equipos ==id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContext.equipos
                                           where e.descripcion.Contains(filtro)
                                           select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try
            {
                _equiposContext.equipos.Add(equipo);
                _equiposContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            try
            {
                equipos? equipoActual = (from e in _equiposContext.equipos
                                         where e.id_equipos == id
                                         select e).FirstOrDefault();
                if (equipoActual == null)
                {
                    return NotFound();
                }
                equipoActual.nombre = equipoModificar.nombre;
                equipoActual.descripcion = equipoModificar.descripcion;
                equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
                equipoActual.marca_id = equipoModificar.marca_id;
                equipoActual.modelo = equipoModificar.modelo;
                equipoActual.anio_compra = equipoModificar.anio_compra;
                equipoActual.costo = equipoModificar.costo;
                equipoActual.vida_util = equipoModificar.vida_util;
                equipoActual.estado_equipo_id = equipoModificar.estado_equipo_id;
                equipoActual.estado = equipoModificar.estado;
                _equiposContext.Entry(equipoActual).State = EntityState.Modified;
                _equiposContext.SaveChanges();
                return Ok(equipoModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            try
            {
                equipos? equipo = (from e in _equiposContext.equipos
                                   where e.id_equipos == id
                                   select e).FirstOrDefault();
                if (equipo == null)
                {
                    return NotFound();
                }
                _equiposContext.equipos.Attach(equipo);
                _equiposContext.equipos.Remove(equipo);
                _equiposContext.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
