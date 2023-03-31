using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Helper;
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
    public class InvoiceHeaderController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public InvoiceHeaderController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpPost]

        public async Task<ActionResult<List<InvoiceHeader>>> AddInvoice(InvoiceHeader invoice)
        {
            _context.InvoiceHeaders.Add(invoice);
            await _context.SaveChangesAsync();


            return Ok(await _context.InvoiceHeaders.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.InvoiceHeaders
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.InvoiceHeaders.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<InvoiceHeader>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var header = await _context.InvoiceHeaders.Where(a => a.ID == id).FirstOrDefaultAsync();
            if (header == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<InvoiceHeader>(header));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<InvoiceHeader>> DeleteHeader(int id)
        {
            var result = await _context.InvoiceHeaders
            .FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _context.InvoiceHeaders.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<InvoiceHeader>> UpdateInvoice(InvoiceHeader item)
        {
            var result = await _context.InvoiceHeaders
            .FirstOrDefaultAsync(e => e.ID == item.ID);

            if (result != null)
            {
                result.OrderID = item.OrderID;
                result.InvoiceID = item.InvoiceID;
                result.Date = item.Date;
                result.Number = item.Number;
                result.Amount = item.Amount;
                result.StatusID = item.StatusID;
                result.WaybillNumber = item.WaybillNumber;
                result.PaymentDate = item.PaymentDate;


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
