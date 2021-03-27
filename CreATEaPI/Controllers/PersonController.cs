using CreATEaPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CreATEaPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        PersonsContext db;
        public PersonController(PersonsContext context)
        {
            db = context;
            if (!db.Persons.Any())
            {
                db.Persons.Add(new Person { firstName = "Tom", lastName = "dsag", age = 26, id = 1, birthday = new DateTime(2008, 5, 1, 8, 30, 52), profession = "IT"  });
                db.Persons.Add(new Person { firstName = "Tom1", lastName = "dsa1g", age = 2632, id = 2, birthday = new DateTime(2008, 5, 1, 8, 30, 52), profession = "IT1" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await db.Persons.ToListAsync();
        }

        // GET api/persons/{{id}}
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            Person user = await db.Persons.FirstOrDefaultAsync(x => x.id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/persons
        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            db.Persons.Add(person);
            await db.SaveChangesAsync();
            return Ok(person);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Person>> Put(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            if (!db.Persons.Any(x => x.id == person.id))
            {
                return NotFound();
            }

            db.Update(person);
            await db.SaveChangesAsync();
            return Ok(person);
        }

        // DELETE api/users/{{id}}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            Person person = db.Persons.FirstOrDefault(x => x.id == id);
            if (person == null)
            {
                return NotFound();
            }
            db.Persons.Remove(person);
            await db.SaveChangesAsync();
            return Ok(person);
        }
    }
}
