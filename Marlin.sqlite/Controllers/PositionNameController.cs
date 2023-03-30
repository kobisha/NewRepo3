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
    public class PositionNameController : ControllerBase
    {
        private readonly DataContext _context;

        public PositionNameController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<PositionName>>> AddPositionName(PositionName name)
        {
            _context.PositionNames.Add(name);
            await _context.SaveChangesAsync();

            return Ok(await _context.PositionNames.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetPositionNames([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.PositionNames
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.PositionNames.CountAsync();

            return Ok(new PagedResponse<List<PositionName>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPositionName(int id)
        {
            var user = await _context.PositionNames.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<PositionName>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PositionName>> DeletePositionName(int id)
        {
            var result = await _context.PositionNames
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.PositionNames.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<PositionName>> UpdatePositionName(PositionName item)
        {
            var result = await _context.PositionNames
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.PriceTypeID = item.PriceTypeID;
                result.Name = item.Name;
                



                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
