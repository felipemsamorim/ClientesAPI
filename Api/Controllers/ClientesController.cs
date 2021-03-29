using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Api.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService ClienteService;

        public ClientesController(IClienteService ClienteService)
        {
            this.ClienteService = ClienteService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return Ok(await ClienteService.GetCliente());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id é invalido");
            }

            return Ok(await ClienteService.GetCliente(long.Parse(id)));
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Cliente>> InsertCliente([FromBody] Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.nome) || string.IsNullOrEmpty(cliente.CPF))
            {
                return BadRequest("Nome do Cliente ou CPF invalido.");
            }

            return Ok(await ClienteService.InsertCliente(cliente));
        }

        [HttpPut]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Cliente>> UpdateCliente([FromBody] Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.id.ToString()))
            {
                return BadRequest("Cliente Id é invalido.");
            }

            if (string.IsNullOrEmpty(cliente.nome) || string.IsNullOrEmpty(cliente.CPF))
            {
                return BadRequest("Cliente Name or Cliente Power are invalid.");
            }

            return Ok(await ClienteService.UpdateCliente(new Cliente(cliente.id, cliente.nome, cliente.CPF, cliente.telefone)));
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id invalido");
            }

            return Ok(await ClienteService.DeleteCliente(long.Parse(id)));
        }
    }
}