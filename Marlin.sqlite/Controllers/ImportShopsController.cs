using Marlin.sqlite.Data;

using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportShopsController : ControllerBase
    {
        private readonly DataContext _context;

        public ImportShopsController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // Read data from local JSON file
            string filePath = "Shops.json";
            string jsonData = System.IO.File.ReadAllText(filePath);
            JObject data = JObject.Parse(jsonData);

            // Extract data for Table1
            var table1Data = data["json"].ToObject<List<Shops>>();

            // Extract data for Table2
            // var table2Data = data["Products"].ToObject<List<OrderDetails>>();

            // Insert data into Table1
            _context.Shops.AddRange(table1Data);

            // Insert data into Table2
            //_context.OrderDetails.AddRange(table2Data);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok();
        }
    
}
}
