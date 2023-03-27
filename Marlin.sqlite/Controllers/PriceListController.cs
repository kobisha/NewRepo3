using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly DataContext _context;

        public PriceListController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<PriceList>>> AddOrder(PriceList order)
        {
            _context.PriceList.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.PriceList.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<List<PriceList>>> GetOrders()
        {
            return Ok(await _context.PriceList.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<PriceList>>> GetOrder(int id)
        {
            var order = await _context.PriceList.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }
            return Ok(order);
        }
    }
}
