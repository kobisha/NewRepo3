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
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public UsersController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }
        [HttpPost]

        public async Task<ActionResult<List<Users>>> AddUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> GetUsers([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Users
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Users>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.Where(a => a.id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new Response<Users>(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var result = await _context.Users
            .FirstOrDefaultAsync(e => e.id == id);
            if (result != null)
            {
                _context.Users.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<Users>> UpdateUsers(Users item)
        {
            var result = await _context.Users
            .FirstOrDefaultAsync(e => e.id == item.id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.UserID = item.UserID;
                result.FirstName = item.FirstName;
                result.LastName = item.LastName;
                result.ContactNumber = item.ContactNumber;
                result.Email = item.Email;
                result.Description = item.Description;
                result.PositionInCompany = item.PositionInCompany;
                result.Password = item.Password;




                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
