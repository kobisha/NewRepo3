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
    public class AccountSettingsController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountSettingsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<AccountSettings>>> AddSettings(AccountSettings settings)
        {
            _context.AccountSettings.Add(settings);
            await _context.SaveChangesAsync();

            return Ok(await _context.AccountSettings.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.AccountSettings
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.AccountSettings.CountAsync();

            return Ok(new PagedResponse<List<AccountSettings>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetSetting(int id)
        {
            var user = await _context.AccountSettings.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<AccountSettings>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AccountSettings>> DeleteSettings(int id)
        {
            var result = await _context.AccountSettings
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.AccountSettings.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<AccountSettings>> UpdateSettings(AccountSettings item)
        {
            var result = await _context.AccountSettings
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.BuyerWS = item.BuyerWS;
                result.SupplierWS = item.SupplierWS;
                result.BillingSettings = item.BillingSettings;


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
