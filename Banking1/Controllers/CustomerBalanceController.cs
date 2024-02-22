using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banking1.Data;
using Banking1.Models;
using AutoMapper;
using Banking1.Dto;

namespace Banking1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBalanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CustomerPointsController _customerpoint;
        public CustomerBalanceController(ApplicationDbContext context, IMapper mapper, CustomerPointsController customerpoint)
        {
            _mapper = mapper;
            _context = context;
            _customerpoint = customerpoint;
        }   

       // GET: api/CustomerBalances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerBalance>>> GetCustomerBalance()
        {
          if (_context.CustomerBalance == null)
          {
              return NotFound();
          }
          var account=_mapper.Map<List<CustomerBalanceDto>>(_context.CustomerBalance.ToList());
            return Ok(account); 
        }

        // GET: api/CustomerBalances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBalance>> GetCustomerBalance(int id)
        {
          if (_context.CustomerBalance == null)
          {
              return NotFound();
          }
            var customerBalance = await _context.CustomerBalance.FindAsync(id);
            var account = _mapper.Map<CustomerBalanceDto>(customerBalance);
            if (customerBalance == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/CustomerBalances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerBalance(int id, CustomerBalance customerBalance)
        {
            if (id != customerBalance.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerBalance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerBalanceExists(id))
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

        // POST: api/CustomerBalances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerBalance>> PostCustomerBalance(CustomerBalanceDto AccountCreate)
        {
          if (_context.CustomerBalance == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CustomerBalance'  is null.");
          }
          var accountMap=_mapper.Map<CustomerBalance>(AccountCreate);   
            _context.CustomerBalance.Add(accountMap);
            await _context.SaveChangesAsync();
            int points = 0;
            if (accountMap.AccountBalance >= 1000 && accountMap.AccountBalance < 5000)
            {
                points = 500;
            }
            if (accountMap.AccountBalance >= 5000)
            {
                points = 1000;
            }
            var lol = new CustomerPoint()
            {
                Points = points,
                //AccountId = accountMap.Id,
                customerid = 1
            };
            _customerpoint.PostCustomerPoint(lol);
            return CreatedAtAction("GetCustomerBalance", new { id = AccountCreate.Id }, AccountCreate);
        }

        // DELETE: api/CustomerBalances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerBalance(int id)
        {
            if (_context.CustomerBalance == null)
            {
                return NotFound();
            }
            var customerBalance = await _context.CustomerBalance.FindAsync(id);
            if (customerBalance == null)
            {
                return NotFound();
            }

            _context.CustomerBalance.Remove(customerBalance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerBalanceExists(int id)
        {
            return (_context.CustomerBalance?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
