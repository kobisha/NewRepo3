using Marlin.sqlite.Data;
using Marlin.sqlite.Migrations;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoiceDetailsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<InvoiceDetail>>> AddInvoicees(InvoiceDetail invoice)
        {
             _context.InvoiceDetails.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(await _context.InvoiceDetails.ToListAsync());
        }

        [HttpGet]

        public async Task<ActionResult<List<InvoiceDetail>>> GetInvoices()
        {
            return Ok(await _context.InvoiceDetails.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<InvoiceDetail>>> GetInvoice(int id)
        {
            var details = await _context.InvoiceDetails.FindAsync(id);
            if (details == null)
            {
                return BadRequest("Invoice Don't Found");
            }
            return Ok(details);
        }
    }
}
