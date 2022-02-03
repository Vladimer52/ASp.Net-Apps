using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserContext db;
        public UserController(UserContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Name = "Tom", Age = 26 });
                db.Users.Add(new User { Name = "Alice", Age = 31 });
                db.SaveChanges();
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if(user == null)
                return BadRequest();
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if(user==null)
                return BadRequest();
            if(!db.Users.Any(x => x.Id == user.Id))
                return NotFound();
            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if(user==null)
                return NotFound();
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
