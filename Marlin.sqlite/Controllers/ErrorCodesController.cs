using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Wrappers;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marlin.sqlite.Services;
using Marlin.sqlite.Helper;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorCodesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public ErrorCodesController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<ErrorCodes>>> AddCodes(ErrorCodes codes)
        {
            _context.ErrorCodes.Add(codes);
            await _context.SaveChangesAsync();

            return Ok(await _context.ErrorCodes.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.ErrorCodes
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.ErrorCodes.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ErrorCodes>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCodes(int id)
        {
            var user = await _context.ErrorCodes.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<ErrorCodes>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ErrorCodes>> DeleteCodes(int id)
        {
            var result = await _context.ErrorCodes
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.ErrorCodes.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<ErrorCodes>> UpdateCodes(ErrorCodes item)
        {
            var result = await _context.ErrorCodes
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.Code = item.Code;
                result.Description = item.Description;



                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
