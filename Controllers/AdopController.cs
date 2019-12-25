using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptionApi.Data;
using AdoptionApi.Models;
using System.Net;

namespace AdoptionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdopController : ControllerBase
    {
        private readonly DataContext _context;

        public AdopController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Adop
        [HttpGet]
        public async Task<IActionResult> GetAdopItem()
        {
            IEnumerable<AdopItem> item;
            item = await _context.AdopItem.OrderBy(x => x.CreatedAt).Include("ItemImage").ToListAsync();
            return Ok(item);
        }


        // GET: by Categpry
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> adop(string type)
        {
            IEnumerable<AdopItem> item;
            item = await _context.AdopItem.OrderBy(x => x.CreatedAt).Where(x => x.Type == type).Include("ItemImage").ToListAsync();
            return Ok(item);
        }


        // GET: api/Adop/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdopItem(int id)
        {

            var adopItem = await _context.AdopItem.Include("ItemImage").FirstOrDefaultAsync(x => x.Id == id);

            if (adopItem == null)
            {
                return NotFound();
            }

            return Ok(adopItem);
        }

        // PUT: api/Adop/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdopItem(int id, AdopItem adopItem)
        {
            if (id != adopItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(adopItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdopItemExists(id))
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

        // POST: api/Adop
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("add")]
        public async Task<IActionResult> PostAdopItem(AdopItem adopItem)
        {
            bool i = UsersExists(adopItem.UsersId);

            if(i)
            {
                _context.AdopItem.Add(adopItem);
                await _context.SaveChangesAsync();
                return StatusCode(201);
            }

            return StatusCode(401);
        }

        // DELETE: api/Adop/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdopItem>> DeleteAdopItem(int id)
        {
            var adopItem = await _context.AdopItem.FindAsync(id);
            if (adopItem == null)
            {
                return NotFound();
            }

            _context.AdopItem.Remove(adopItem);
            await _context.SaveChangesAsync();

            return adopItem;
        }

        private bool AdopItemExists(int id)
        {
            return _context.AdopItem.Any(e => e.Id == id);
        }

        private bool UsersExists(int id)
        {
            return  _context.Users.Any(e => e.Id == id);
        }

    }
}
