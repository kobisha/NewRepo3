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
        public async Task<IActionResult> GetOrders([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.PriceList
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.PriceList.CountAsync();

            return Ok(new PagedResponse<List<PriceList>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var header = await _context.PriceList.Where(a => a.ID == id).FirstOrDefaultAsync();
            if (header == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<PriceList>(header));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PriceList>> DeletePriceList(int id)
        {
            var result = await _context.PriceList
            .FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _context.PriceList.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<PriceList>> UpdatePriceList(PriceList item)
        {
            var result = await _context.PriceList
            .FirstOrDefaultAsync(e => e.ID == item.ID);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.Date = item.Date;
                result.ProductID = item.ProductID;
                result.PriceType = item.PriceType;
                result.Unit = item.Unit;
                result.Price = item.Price;




                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
