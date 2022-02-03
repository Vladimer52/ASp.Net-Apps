using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
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
                db.Users.Add(new User { Name = "Bob", Age= 32 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("Id")]
        public async Task<ActionResult> Get(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Post(User user)
        {

            if (user.Age == 99)
                ModelState.AddModelError("Age", "Возраст не должен быть равен 99");
            if (user.Name == "admin")
                ModelState.AddModelError("Name", "недопустимое имя - admin");
            if(!ModelState.IsValid) //если есть ошибки, возвращаем код ошибки
                return BadRequest(ModelState);

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<User>>> Put(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            if(!db.Users.Any(x=> x.Id == user.Id))
            {
                return NotFound();
            }
            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<IEnumerable<User>>> Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if(user == null)
                return NotFound();
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
