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
    public class UserPositionsController : ControllerBase
    {
        private readonly DataContext _context;

        public UserPositionsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<UserPositions>>> AddUserPositions(UserPositions positions)
        {
            _context.UserPositions.Add(positions);
            await _context.SaveChangesAsync();

            return Ok(await _context.UserPositions.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetUserPositions([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.UserPositions
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.UserPositions.CountAsync();

            return Ok(new PagedResponse<List<UserPositions>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserPosition(int id)
        {
            var user = await _context.UserPositions.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<UserPositions>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserPositions>> DeleteUserPositions(int id)
        {
            var result = await _context.UserPositions
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.UserPositions.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<UserPositions>> UpdateUserPositions(UserPositions item)
        {
            var result = await _context.UserPositions
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.PositionID = item.PositionID;
                result.PositionName = item.PositionName;
                




                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
