using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Models;
using Marlin.sqlite.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeaderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderHeaderController(DataContext context)
        {
           _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderHeader>>> AddOrder(OrderHeader order)
        {
            _context.OrderHeaders.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderHeaders.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.OrderHeaders
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.OrderHeaders.CountAsync();

            return Ok(new PagedResponse<List<OrderHeader>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var header = await _context.OrderHeaders.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (header == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<OrderHeader>(header));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderHeader>> DeleteDetals(int id)
        {
            var result = await _context.OrderHeaders
            .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.OrderHeaders.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<OrderHeader>> UpdateInvoice(OrderHeader item)
        {
            var result = await _context.OrderHeaders
            .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.OrderID = item.OrderID;
                result.SourceID = item.SourceID;
                result.Date = item.Date;
                result.Number = item.Number;
                result.SenderID = item.SenderID;
                result.ReceiverID = item.ReceiverID;
                result.ShopID = item.ShopID;
                result.Amount = item.Amount;
                result.StatusID = item.StatusID;



                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
