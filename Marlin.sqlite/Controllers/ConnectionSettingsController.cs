using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Wrappers;
using Marlin.sqlite.Models;
using Marlin.sqlite.Services;
using Marlin.sqlite.Helper;
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
        private readonly IUriService _uriService;

        public ConnectionSettingsController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<ConnectionSettings>>> AddSettings(ConnectionSettings settings)
        {
            _context.ConnectionSettings.Add(settings);
            await _context.SaveChangesAsync();

            return Ok(await _context.ConnectionSettings.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.ConnectionSettings
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.ConnectionSettings.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ConnectionSettings>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetSetting(int id)
        {
            var user = await _context.ConnectionSettings.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<ConnectionSettings>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ConnectionSettings>> DeleteSettings(int id)
        {
            var result = await _context.ConnectionSettings
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.ConnectionSettings.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<ConnectionSettings>> UpdateSettings(ConnectionSettings item)
        {
            var result = await _context.ConnectionSettings
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
