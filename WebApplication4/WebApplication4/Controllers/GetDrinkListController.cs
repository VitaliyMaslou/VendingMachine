using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDrinkListController : ControllerBase
    {
        private readonly VendingmachinedatabaseContext _context;

        public GetDrinkListController(VendingmachinedatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<List<Drink>>>> Get(int id)
        {
            List<Drink> drinkslist = new List<Drink>();
            var VM = _context.VendingMachineDrinks.Where(x => x.VendingMachineId == id && x.Count > 0).ToList();
            foreach (var item in VM)
            {
                Drink drink = await _context.Drinks.FirstOrDefaultAsync(x => x.Id == item.DrinksId);
                if (drink != null)
                {
                    drinkslist.Add(drink);
                }
            }

            if (drinkslist.Count == 0)
            {
                return NotFound();
            }
            return new ObjectResult(drinkslist);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Coin>>> PutCoin(int id, int drinkid)
        {
            var DR = await _context.VendingMachineDrinks.FirstOrDefaultAsync(x => x.VendingMachineId == id && x.DrinksId == drinkid);

            _context.Entry(DR).State = EntityState.Modified;
            try
            {
                if(DR.Count==0)
                {
                    return NotFound();
                }
                DR.Count--;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdExists(id))
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

        private bool IdExists(int id)
        {
            return _context.VendingMachineDrinks.Any(e => e.VendingMachineId == id);
        }
    }
   

}

