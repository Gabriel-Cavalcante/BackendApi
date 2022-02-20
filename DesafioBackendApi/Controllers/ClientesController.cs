using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioBackendApi.Context;
using DesafioBackendApi.Models;
using System.Text.RegularExpressions;

namespace DesafioBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var cliente = await _context.Clientes.ToListAsync();

            if (!cliente.Any())
            {
                return NotFound();
            }

            return await _context.Clientes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                if (_context.Clientes.Any(ac => ac.Cpf.Equals(cliente.Cpf) && ac.Id != cliente.Id)
                    || _context.Clientes.Any(ac => ac.Email.Equals(cliente.Email) && ac.Id != cliente.Id))
                {
                    return BadRequest();
                }
                else
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return cliente;
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (_context.Clientes.Where(c => c.Cpf == cliente.Cpf).Any() || _context.Clientes.Where(e => e.Email == cliente.Email).Any())
                {
                    return BadRequest();
                }
                else
                {
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
