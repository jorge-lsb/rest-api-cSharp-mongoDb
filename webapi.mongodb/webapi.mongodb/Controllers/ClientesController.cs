using Microsoft.AspNetCore.Mvc;
using webapi.mongodb.Data;
using webapi.mongodb.Entitys;

namespace webapi.mongodb.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var cliente = _clienteService.GetById(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            _clienteService.Create(cliente);

            return CreatedAtRoute("GetCliente", new
            {
                id = cliente.Id.ToString()
            }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Cliente cliente)
        {
            var c = _clienteService.GetById(id);

            if (c == null)
                NotFound();

            _clienteService.Update(id, cliente);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Cliente cliente)
        {
            var c = _clienteService.GetById(cliente.Id);

            if (c == null)
                NotFound();

            _clienteService.Delete(cliente);

            return NoContent();
        }

    }
}
