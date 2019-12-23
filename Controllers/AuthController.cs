using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptionApi.Data;
using AdoptionApi.Models;
using AdoptionApi.Dtos;

namespace AdoptionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuth _repo;
        public AuthController(IAuth repo,DataContext context)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Auth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Auth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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


        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }




        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }






        // POST: api/Auth
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("login")]
        public async Task<IActionResult> login(UserLogin users)
        {

            var user = await _repo.Login(users.Email, users.Password);

            if (user == null)
            {
                return Unauthorized();
            }


          
            return Ok(user);
            
        }




        // POST: api/Auth
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("register")]
        public async Task<IActionResult> PostUsers(UserRegister users)
        {

            users.Email = users.Email.ToLower();

            if (await _repo.UserExists(users.Email))
            {
                return BadRequest("Username already taken");
            }

            else
            {
                var userToCreate = new Users
                {
                    Email = users.Email,
                    PhoneNumber = users.PhoneNumber,
                    Name = users.Name
                };

                var createdUser = await _repo.Register(userToCreate, users.Password);

                return StatusCode(201);
            }

        }







    }
}
