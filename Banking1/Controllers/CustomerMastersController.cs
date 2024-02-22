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
    public class CustomerMastersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerMastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerMaster>>> GetCustomerMaster()
        {
          if (_context.CustomerMaster == null)
          {
              return NotFound();
          }
            return await _context.CustomerMaster.ToListAsync();
        }

        // GET: api/CustomerMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerMaster>> GetCustomerMaster(int id)
        {
          if (_context.CustomerMaster == null)
          {
              return NotFound();
          }
            var customerMaster = await _context.CustomerMaster.FindAsync(id);

            if (customerMaster == null)
            {
                return NotFound();
            }

            return customerMaster;
        }

        // PUT: api/CustomerMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerMaster(int id, CustomerMaster customerMaster)
        {
            if (id != customerMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerMasterExists(id))
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

        // POST: api/CustomerMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerMaster>> PostCustomerMaster(CustomerMaster customerMaster)
        {
          if (_context.CustomerMaster == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CustomerMaster'  is null.");
          }
            _context.CustomerMaster.Add(customerMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerMaster", new { id = customerMaster.Id }, customerMaster);
        }

        // DELETE: api/CustomerMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerMaster(int id)
        {
            if (_context.CustomerMaster == null)
            {
                return NotFound();
            }
            var customerMaster = await _context.CustomerMaster.FindAsync(id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            _context.CustomerMaster.Remove(customerMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerMasterExists(int id)
        {
            return (_context.CustomerMaster?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
