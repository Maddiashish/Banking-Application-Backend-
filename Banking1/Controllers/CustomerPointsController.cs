using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banking1.Data;
using Banking1.Models;

namespace Banking1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPointsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerPointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPoint>>> GetCustomerPoint()
        {
          if (_context.CustomerPoint == null)
          {
              return NotFound();
          }
            return await _context.CustomerPoint.ToListAsync();
        }

        // GET: api/CustomerPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPoint>> GetCustomerPoint(int id)
        {
          if (_context.CustomerPoint == null)
          {
              return NotFound();
          }
            var customerPoint = await _context.CustomerPoint.FindAsync(id);

            if (customerPoint == null)
            {
                return NotFound();
            }

            return customerPoint;
        }

        // PUT: api/CustomerPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPoint(int id, CustomerPoint customerPoint)
        {
            if (id != customerPoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerPoint>> PostCustomerPoint(CustomerPoint customerPoint)
        {
          if (_context.CustomerPoint == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CustomerPoint'  is null.");
          }
            _context.CustomerPoint.Add(customerPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPoint", new { id = customerPoint.Id }, customerPoint);
        }

        // DELETE: api/CustomerPoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPoint(int id)
        {
            if (_context.CustomerPoint == null)
            {
                return NotFound();
            }
            var customerPoint = await _context.CustomerPoint.FindAsync(id);
            if (customerPoint == null)
            {
                return NotFound();
            }

            _context.CustomerPoint.Remove(customerPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPointExists(int id)
        {
            return (_context.CustomerPoint?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
