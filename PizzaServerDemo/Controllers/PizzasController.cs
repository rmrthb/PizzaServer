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
    public class PizzasController : ControllerBase
    {
        private readonly PizzaServerDemoContext _context;

        public PizzasController(PizzaServerDemoContext context)
        {
            _context = context;
        }

        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizza()
        {
            //var products = await _context.Product.Include(c => c.category).Where(c => c.categoryId == categoryId).ToListAsync();

            return await _context.Pizza.ToListAsync();
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            //var pizza = await _context.Pizza.Include(p => p.PizzaOrders).Where(p => p.PizzaID == id).FindAsync(id);

            //var pizza = await _context.Pizza.FindAsync(id);
            //var pizza = await _context.Pizza.Include(p => p.PizzaOrders).Where(p => p.PizzaID == id).FirstOrDefaultAsync();
            //LÖSNINGEN
            var pizza = await _context.Pizza.Include(po => po.PizzaOrders)
            .ThenInclude(o => o.Order)
            .Select(s => new { s.PizzaID, s.PizzaName, order = s.PizzaOrders.Select(o => new { o.Order.OrderID, o.Order.TotalCost, o.Order.PizzaOrders }) })
            .ToListAsync();

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
            //return pizza;
        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.PizzaID)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
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

        // POST: api/Pizzas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            _context.Pizza.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizza", new { id = pizza.PizzaID }, pizza);
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pizza>> DeletePizza(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();

            return pizza;
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.PizzaID == id);
        }
    }
}
