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
    public class OrderDetailsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public OrderDetailsController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderDetails>>> AddOrder(OrderDetails order)
        {
            _context.OrderDetails.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderDetails.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.OrderDetails
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.OrderDetails.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<OrderDetails>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var header = await _context.OrderDetails.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (header == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<OrderDetails>(header));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderDetails>> DeleteDetals(int id)
        {
            var result = await _context.OrderDetails
            .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.OrderDetails.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<OrderDetails>> UpdateInvoice(OrderDetails item)
        {
            var result = await _context.OrderDetails
            .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.OrderID = item.OrderID;
                result.ProductID = item.ProductID;
                result.Unit = item.Unit;
                result.Quantity = item.Quantity;
                result.Price = item.Price;
                result.Amount = item.Amount;
                result.ReservedQuantity = item.ReservedQuantity;
               


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
