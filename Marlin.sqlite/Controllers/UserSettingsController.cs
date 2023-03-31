using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Wrappers;
using Marlin.sqlite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marlin.sqlite.Services;
using Marlin.sqlite.Helper;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public UserSettingsController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<UserSettings>>> AddUserSettings(UserSettings settings)
        {
            _context.UserSettings.Add(settings);
            await _context.SaveChangesAsync();

            return Ok(await _context.UserSettings.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetUserSettings([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.UserSettings
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.UserSettings.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserSettings>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserSetting(int id)
        {
            var user = await _context.UserSettings.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<UserSettings>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserSettings>> DeleteUserSettings(int id)
        {
            var result = await _context.UserSettings
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.UserSettings.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<UserSettings>> UpdateUserSettings(UserSettings item)
        {
            var result = await _context.UserSettings
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.UserID = item.UserID;
                result.ProfileID = item.ProfileID;
                



                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
