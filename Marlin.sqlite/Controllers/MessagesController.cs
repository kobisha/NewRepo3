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
    public class MessagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public MessagesController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<Messages>>> AddMessages(Messages messages)
        {
            _context.Messages.Add(messages);
            await _context.SaveChangesAsync();

            return Ok(await _context.Messages.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetMessages([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Messages
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.Messages.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Messages>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetMessage(int id)
        {
            var user = await _context.Messages.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<Messages>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Messages>> DeleteMessages(int id)
        {
            var result = await _context.Messages
            .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.Messages.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<Messages>> UpdateMessages(Messages item)
        {
            var result = await _context.Messages
            .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.MessageID = item.MessageID;
                result.Date = item.Date;
                result.SenderID = item.SenderID;
                result.ReceiverID = item.ReceiverID;
                result.Type = item.Type;
                result.JSONBody = item.JSONBody;
                


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
