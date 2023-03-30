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
    public class ConnectionSettingsController : ControllerBase
    {
        private readonly DataContext _context;

        public ConnectionSettingsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<ConnectionSettings>>> AddSettings(ConnectionSettings settings)
        {
            _context.ConnectionSetting.Add(settings);
            await _context.SaveChangesAsync();

            return Ok(await _context.ConnectionSetting.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.ConnectionSetting
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.ConnectionSetting.CountAsync();

            return Ok(new PagedResponse<List<ConnectionSettings>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetSetting(int id)
        {
            var user = await _context.ConnectionSetting.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<ConnectionSettings>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ConnectionSettings>> DeleteSettings(int id)
        {
            var result = await _context.ConnectionSetting
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.ConnectionSetting.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<ConnectionSettings>> UpdateSettings(ConnectionSettings item)
        {
            var result = await _context.ConnectionSetting
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.ConnectedAccountID = item.ConnectedAccountID;
                result.AsBuyer = item.AsBuyer;
                result.AsSupplier = item.AsSupplier;
                result.PriceTypes = item.PriceTypes;
                result.ConnectionStatus = item.ConnectionStatus;


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
