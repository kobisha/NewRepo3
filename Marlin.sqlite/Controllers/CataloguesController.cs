using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CataloguesController : ControllerBase
    {
        private readonly DataContext _context;

        public CataloguesController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<List<Catalogues>>>AddCatalog(Catalogues item)
        {
            _context.Catalogues.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.Catalogues.ToListAsync());

        }

        [HttpGet]
        
        public async Task<ActionResult<List<Catalogues>>>Getcatalogs()
        {
            return Ok(await _context.Catalogues.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<Catalogues>>>Getcatalog(int id)
        {
            var item = await _context.Catalogues.FindAsync(id);
            if (item == null)
            {
                return BadRequest("Item Not Found");
            }
            return Ok(item);
        }
    }
}
