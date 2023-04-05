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
    public class OrderHeaderController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public OrderHeaderController(DataContext context, IUriService uriService)
        {
           _context = context;
            _uriService = uriService;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderHeaders>>> AddOrder(OrderHeaders order)
        {
            _context.OrderHeaders.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderHeaders.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.OrderHeaders
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.OrderHeaders.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<OrderHeaders>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var header = await _context.OrderHeaders.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (header == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<OrderHeaders>(header));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderHeaders>> DeleteDetals(int id)
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
        public async Task<ActionResult<OrderHeaders>> UpdateInvoice(OrderHeaders item)
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
