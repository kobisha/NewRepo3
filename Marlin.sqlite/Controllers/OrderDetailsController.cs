using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
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

        public OrderDetailsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderDetails>>> AddOrder(OrderDetails order)
        {
            _context.OrderDetails.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderDetails.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<List<OrderDetails>>> GetOrders()
        {
            return Ok(await _context.OrderDetails.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<OrderDetails>>> GetOrder(int id)
        {
            var order = await _context.OrderDetails.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }
            return Ok(order);
        }
    }
}
