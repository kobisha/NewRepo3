using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Wrappers;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly DataContext _context;

        public ShopsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<Shops>>> AddShops(Shops shops)
        {
            _context.Shops.Add(shops);
            await _context.SaveChangesAsync();

            return Ok(await _context.Shops.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetShops([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Shops
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.Shops.CountAsync();

            return Ok(new PagedResponse<List<Shops>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetShop(int id)
        {
            var user = await _context.Shops.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<Shops>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Shops>> DeleteShops(int id)
        {
            var result = await _context.Shops
            .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.Shops.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<Shops>> UpdateShops(Shops item)
        {
            var result = await _context.Shops
            .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.ShopID = item.ShopID;
                result.SourceCode = item.SourceCode;
                result.Name = item.Name;
                result.Description = item.Description;
                result.Address = item.Address;
                result.ContactPerson = item.ContactPerson;
                result.ContactNumber = item.ContactNumber;
                result.Email = item.Email;
                result.Region = item.Region;
                result.Format = item.Format;
                result.GPS = item.GPS;




                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
