using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<ActionResult<List<Accounts>>>AddUser(Accounts user)
        {
            _context.Accounts.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpGet]

        public async Task<ActionResult<List<Accounts>>> GetUsers()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Accounts>>GetUser(int id)
        {
            var user = await _context.Accounts.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(user);
        }
    }
}
