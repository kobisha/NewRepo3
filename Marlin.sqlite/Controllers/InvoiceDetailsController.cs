using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Helper;
using Marlin.sqlite.Migrations;
using Marlin.sqlite.Models;
using Marlin.sqlite.Services;
using Marlin.sqlite.Wrappers;
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
        private readonly IUriService _uriService;

        public InvoiceDetailsController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpPost]

        public async Task<ActionResult<List<InvoiceDetail>>> AddInvoicees(InvoiceDetail invoice)
        {
             _context.InvoiceDetails.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(await _context.InvoiceDetails.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetInvoices([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.InvoiceDetails
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.InvoiceDetails.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<InvoiceDetail>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var user = await _context.InvoiceDetails.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<InvoiceDetail>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<InvoiceDetail>> DeleteInvoice(int id)
        {
            var result = await _context.InvoiceDetails
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.InvoiceDetails.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<InvoiceDetail>> UpdateInvoice(InvoiceDetail item)
        {
            var result = await _context.InvoiceDetails
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.InvoiceID = item.InvoiceID;
                result.ProductID = item.InvoiceID;
                result.Unit = item.Unit;
                result.Quantity = item.Quantity;
                result.Price = item.Price;
                result.Amount = item.Amount;
               

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
