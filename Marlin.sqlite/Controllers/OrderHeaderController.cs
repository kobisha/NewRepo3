using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
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
        public async Task<ActionResult<List<OrderHeader>>> GetOrders()
        {
            return Ok(await _context.OrderHeaders.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<OrderHeader>>> GetOrder(int id)
        {
            var order = await _context.OrderHeaders.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }
            return Ok(order);
        }
    }
}
