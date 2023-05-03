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
    public class ProductsByCategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUriService _uriService;

        public ProductsByCategoriesController(DataContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }


        [HttpPost]
        public async Task<ActionResult<List<ProductsByCategories>>> AddProduct(ProductsByCategories item)
        {
            _context.ProductsByCategories.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.ProductsByCategories.ToListAsync());

        }

        [HttpGet]

        public async Task<IActionResult> GetProducts([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.ProductsByCategories
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.ProductsByCategories.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ProductsByCategories>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProduct(int id)
        {
            var item = await _context.ProductsByCategories.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return BadRequest("Item Not Found");
            }
            return Ok(new Response<ProductsByCategories>(item));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductsByCategories>> DeleteProduct(int id)
        {
            var result = await _context.ProductsByCategories
            .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.ProductsByCategories.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        [HttpPut]
        public async Task<ActionResult<ProductsByCategories>> UpdateProduct(ProductsByCategories item)
        {
            var result = await _context.ProductsByCategories
            .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.AccountID = item.AccountID;
                result.ProductID = item.ProductID;
                result.CategoryID = item.CategoryID;
                


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
