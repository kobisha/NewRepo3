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
    public class PriceListController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public PriceListController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
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
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.PriceList
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.PriceList.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<PriceList>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
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
