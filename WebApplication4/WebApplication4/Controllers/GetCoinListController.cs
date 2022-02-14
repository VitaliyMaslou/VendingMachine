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
    public class GetCoinListController : ControllerBase
    {
        private readonly VendingmachinedatabaseContext _context;

        public GetCoinListController(VendingmachinedatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<List<Coin>>>> Get(int id)
        {
            int[] coinslist = new int[4];

            var VM = await _context.VendingMachineCoins.Where(x => x.VendingMachineId == id).ToListAsync();

            for (int i = 0; i < VM.Count; i++)
            {
                coinslist[i] = VM[i].Count;
            }

            return new ObjectResult(coinslist);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Coin>>> PutCoin(int id, int[] countarrau)
        {
            var VM = await _context.VendingMachineCoins.Where(x => x.VendingMachineId == id).ToListAsync();

            for (int i = 0; i < VM.Count; i++)
            {
                _context.Entry(VM[i]).State = EntityState.Modified;
                try
                {
                    VM[i].Count += countarrau[i];

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
            }

            return NoContent();
        }

        private bool IdExists(int id)
        {
            return _context.VendingMachineCoins.Any(e => e.VendingMachineId == id);
        }
    }
}
