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
    public class OrderStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderStatus>>> AddOrder(OrderStatus order)
        {
            _context.OrderStatus.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderStatus.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.OrderStatus  
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.OrderStatus.CountAsync();

            return Ok(new PagedResponse<List<OrderStatus>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var header = await _context.OrderStatus.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (header == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<OrderStatus>(header));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderStatus>> DeleteStatus(int id)
        {
            var result = await _context.OrderStatus
            .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.OrderStatus.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<OrderStatus>> UpdateInvoice(OrderStatus item)
        {
            var result = await _context.OrderStatus
            .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.StatusID = item.StatusID;
                result.StatusName = item.StatusName;
               



                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
