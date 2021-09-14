using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.API.Data;
using Portal.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.API.Controllers
{
    [Route("api/value")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private DataContext _context;

        public ValueController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] Value value)
        {
            _context.Values.Add(value); 
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id, [FromBody] Value value)
        {
            var data = await _context.Values.FindAsync(id);
            data.Name = value.Name;
            _context.Values.Update(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var data = await _context.Values.FindAsync(id);
            _context.Values.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}