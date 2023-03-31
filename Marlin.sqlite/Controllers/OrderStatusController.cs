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
    public class OrderStatusController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public OrderStatusController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
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
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.OrderStatus  
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.OrderStatus.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<OrderStatus>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
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
