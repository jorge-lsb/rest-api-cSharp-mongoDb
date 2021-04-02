using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _clienteService.Get(pageIndex, pageSize));
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
            var result = _clienteService.Create(cliente);

            return Ok(result);
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
