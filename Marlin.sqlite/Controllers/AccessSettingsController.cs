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
    public class AccessSettingsController : ControllerBase
    {
        private readonly DataContext _context;

        public AccessSettingsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<AccessSettings>>> AddSettings(AccessSettings settings)
        {
            _context.AccessSettings.Add(settings);
            await _context.SaveChangesAsync();

            return Ok(await _context.AccessSettings.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.AccessSettings
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.AccessSettings.CountAsync();

            return Ok(new PagedResponse<List<AccessSettings>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetSetting(int id)
        {
            var user = await _context.AccessSettings.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<AccessSettings>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AccessSettings>> DeleteSettings(int id)
        {
            var result = await _context.AccessSettings
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.AccessSettings.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<AccessSettings>> UpdateSettings(AccessSettings item)
        {
            var result = await _context.AccessSettings
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.ProfileID = item.ProfileID;
                result.AccessObject = item.AccessObject;
                result.Grant = item.Grant;


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
