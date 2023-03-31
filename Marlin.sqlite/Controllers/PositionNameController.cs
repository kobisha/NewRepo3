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
    public class PositionNameController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public PositionNameController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<PositionName>>> AddPositionName(PositionName name)
        {
            _context.PositionName.Add(name);
            await _context.SaveChangesAsync();

            return Ok(await _context.PositionName.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetPositionNames([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.PositionName
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.PositionName.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<PositionName>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPositionName(int id)
        {
            var user = await _context.PositionName.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<PositionName>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PositionName>> DeletePositionName(int id)
        {
            var result = await _context.PositionName
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.PositionName.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<PositionName>> UpdatePositionName(PositionName item)
        {
            var result = await _context.PositionName
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
