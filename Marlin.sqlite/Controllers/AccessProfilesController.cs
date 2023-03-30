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
    public class AccessProfilesController : ControllerBase
    {
        private readonly DataContext _context;

        public AccessProfilesController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<AccessProfiles>>> AddProfiles(AccessProfiles profile)
        {
            _context.AccessProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return Ok(await _context.AccessProfiles.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetUsers([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.AccessProfiles
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.AccessProfiles.CountAsync();

            return Ok(new PagedResponse<List<AccessProfiles>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.AccessProfiles.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<AccessProfiles>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AccessProfiles>> DeleteAccount(int id)
        {
            var result = await _context.AccessProfiles
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.AccessProfiles.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<AccessProfiles>> UpdateUser(AccessProfiles item)
        {
            var result = await _context.AccessProfiles
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.ProfileID = item.ProfileID;
                result.ProfileName = item.ProfileName;
                

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
