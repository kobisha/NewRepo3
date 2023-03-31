using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Wrappers;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Marlin.sqlite.Services;
using Marlin.sqlite.Helper;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeLogController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public ExchangeLogController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<ExchangeLog>>> AddLogs(ExchangeLog log)
        {
            _context.ExchangeLog.Add(log);
            await _context.SaveChangesAsync();

            return Ok(await _context.ExchangeLog.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetSettings([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.ExchangeLog
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.ExchangeLog.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ExchangeLog>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetLogs(int id)
        {
            var user = await _context.ExchangeLog.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<ExchangeLog>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ExchangeLog>> DeleteLogs(int id)
        {
            var result = await _context.ExchangeLog
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.ExchangeLog.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<ExchangeLog>> UpdateLogs(ExchangeLog item)
        {
            var result = await _context.ExchangeLog
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
