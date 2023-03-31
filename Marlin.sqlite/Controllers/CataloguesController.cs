using Marlin.sqlite.Data;
using Marlin.sqlite.Filter;
using Marlin.sqlite.Helper;
using Marlin.sqlite.Models;
using Marlin.sqlite.Services;
using Marlin.sqlite.Wrappers;
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
        private readonly IUriService _uriService;

        public CataloguesController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }


        [HttpPost]
        public async Task<ActionResult<List<Catalogues>>>AddCatalog(Catalogues item)
        {
            _context.Catalogues.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.Catalogues.ToListAsync());

        }

        [HttpGet]
        
        public async Task<IActionResult> GetCatalogs([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Catalogues
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.Catalogues.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Catalogues>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Getcatalog(int id)
        {
            var item = await _context.Catalogues.Where(a => a.ID == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return BadRequest("Item Not Found");
            }
            return Ok(new Response<Catalogues>(item));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Catalogues>> DeleteCatalog(int id)
        {
            var result = await _context.Catalogues
            .FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _context.Catalogues.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<Catalogues>> UpdateCatalog(Catalogues item)
        {
            var result = await _context.Catalogues
            .FirstOrDefaultAsync(e => e.ID == item.ID);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.ProductID = item.ProductID;
                result.SourceCode = item.SourceCode;
                result.Name = item.Name;
                result.Description = item.Description;
                result.Barcode = item.Barcode;
                result.Unit = item.Unit;
                result.Status = item.Status;
                result.LastChangeDate = item.LastChangeDate;
               

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
