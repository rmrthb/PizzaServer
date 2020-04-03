using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaServerDemo.Data;
using PizzaServerDemo.Model;

namespace PizzaServerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaOrdersController : ControllerBase
    {
        private readonly PizzaServerDemoContext _context;

        public PizzaOrdersController(PizzaServerDemoContext context)
        {
            _context = context;
        }

        // GET: api/PizzaOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaOrder>>> GetPizzaOrder()
        {
            return await _context.PizzaOrder.ToListAsync();
        }

        // GET: api/PizzaOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaOrder>> GetPizzaOrder(int id)
        {
            var pizzaOrder = await _context.PizzaOrder.FindAsync(id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return pizzaOrder;
        }

        // PUT: api/PizzaOrders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaOrder(int id, PizzaOrder pizzaOrder)
        {
            if (id != pizzaOrder.PizzaID)
            {
                return BadRequest();
            }

            _context.Entry(pizzaOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaOrderExists(id))
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

        // POST: api/PizzaOrders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaOrder>> PostPizzaOrder(PizzaOrder pizzaOrder)
        {
            _context.PizzaOrder.Add(pizzaOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PizzaOrderExists(pizzaOrder.PizzaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPizzaOrder", new { id = pizzaOrder.PizzaID }, pizzaOrder);
        }

        // DELETE: api/PizzaOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaOrder>> DeletePizzaOrder(int id)
        {
            var pizzaOrder = await _context.PizzaOrder.FindAsync(id);
            if (pizzaOrder == null)
            {
                return NotFound();
            }

            _context.PizzaOrder.Remove(pizzaOrder);
            await _context.SaveChangesAsync();

            return pizzaOrder;
        }

        private bool PizzaOrderExists(int id)
        {
            return _context.PizzaOrder.Any(e => e.PizzaID == id);
        }
    }
}
