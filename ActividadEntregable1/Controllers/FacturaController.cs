using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryEf.Data.Models;
using RepositoryEf.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryEf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaRepository _facturaRepository;
        public FacturaController(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }
        // GET: api/<FacturaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                return Ok(_facturaRepository.GetAll());

            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                return Ok(_facturaRepository.GetAll());

            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        // POST api/<FacturaController>
        [HttpPost]
        public IActionResult Post([FromBody] Factura factura)
        {
            _facturaRepository.Create(factura);
            return Ok();
        }
    

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Factura factura)
        {

            try
            {
                _facturaRepository.Update(factura);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente más tarde.");
            }

        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var result = _facturaRepository.GetById(id);
            
            return Ok(result);
        }
    }
}
