using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrestamosApp.BLL;
using PrestamosApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        // GET: api/<PrestamosController>
        [HttpGet]
        public ActionResult<IEnumerable<Prestamos>> Get()
        {
            return PrestamoBLL.Getprestamos();
        }       

            // GET api/<PrestamosController>/5
        [HttpGet("{id}")]
        public ActionResult<Prestamos> Get(int id)
        {
            return PrestamoBLL.Buscar(id);
        }

        // POST api/<PrestamosController>
        [HttpPost]
        public void Post([FromBody] Prestamos prestamos)
        {
            PrestamoBLL.Guardar(prestamos);
        }

        // PUT api/<PrestamosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PrestamosController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return PrestamoBLL.Eliminar(id);
        }
    }
}
