using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marlin.sqlite.Data;
using Marlin.sqlite.JsonModels;
using Marlin.sqlite.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImortJsonController : ControllerBase
    {
        private readonly DataContext _context;

        public ImortJsonController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // Read data from local JSON file
            string filePath = "Marlin-PO.json";
            string jsonData = System.IO.File.ReadAllText(filePath);
            JObject data = JObject.Parse(jsonData);

            // Extract data for Table1
            var table1Data = data["header"].ToObject<List<OrderHeaders>>();

            // Extract data for Table2
            var table2Data = data["Products"].ToObject<List<OrderDetails>>();

            // Insert data into Table1
            _context.OrderHeaders.AddRange(table1Data);

            // Insert data into Table2
            _context.OrderDetails.AddRange(table2Data);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
