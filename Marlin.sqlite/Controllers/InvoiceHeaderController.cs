using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceHeaderController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoiceHeaderController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<InvoiceHeader>>> AddInvoice(InvoiceHeader invoice)
        {
            _context.InvoiceHeaders.Add(invoice);
            await _context.SaveChangesAsync();


            return Ok(await _context.InvoiceHeaders.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<List<InvoiceHeader>>> GetOrders()
        {
            return Ok(await _context.InvoiceHeaders.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<InvoiceHeader>>> GetOrder(int id)
        {
            var order = await _context.InvoiceHeaders.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }
            return Ok(order);
        }
    }
}
