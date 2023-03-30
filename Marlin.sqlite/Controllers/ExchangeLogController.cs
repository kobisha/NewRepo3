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
    public class ExchangeLogController : ControllerBase
    {
        private readonly DataContext _context;

        public ExchangeLogController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<ExchangeLog>>> AddLogs(ExchangeLog log)
        {
            _context.ExchangeLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok(await _context.ExchangeLogs.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.ExchangeLogs
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.ExchangeLogs.CountAsync();

            return Ok(new PagedResponse<List<ExchangeLog>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetLogs(int id)
        {
            var user = await _context.ExchangeLogs.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<ExchangeLog>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ExchangeLog>> DeleteLogs(int id)
        {
            var result = await _context.ExchangeLogs
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.ExchangeLogs.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<ExchangeLog>> UpdateLogs(ExchangeLog item)
        {
            var result = await _context.ExchangeLogs
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.TransactionID = item.TransactionID;
                result.Date = item.Date;
                result.MessageID = item.MessageID;
                result.Status = item.Status;
                result.ErrorCode = item.ErrorCode;



                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
