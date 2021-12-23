using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : ControllerBase
    {
        private readonly RedBookContext redBookContext;
        public ThingController(RedBookContext redBookContext)
        {
            this.redBookContext = redBookContext;
        }


        [Route("List")]
        public IEnumerable<Thing> ThingList()
        {
            return redBookContext.Thing.ToList();
        }


        [Route("CategoryList")]
        public IEnumerable<String> GetCategoryName()
        {
            return redBookContext.Category.Select(thing => thing.Name.Trim()).ToArray();
        }
        [Route("KingdomsList")]
        public IEnumerable<String> GetKingdomsName()
        {
            return redBookContext.Kingdoms.Select(thing => thing.Name.Trim()).ToArray();
        }


        [Route("ClassList")]
        public IEnumerable<Class> GetClassList(string Kingdom)
        {
            if (Kingdom == null)
            {
                return redBookContext.Class.ToArray();
            }
            else
            {
                return redBookContext.Class.Where(x => x.Kingdom.Name == Kingdom).ToArray();
            }
            //return redBookContext.Class.Select(x => thing.Name.Trim()).ToArray();
        }


        [Route("{id}")]
        public ActionResult<Thing> GetProductById(int id)
        {
            var thing = redBookContext.Thing.FirstOrDefault(thing => thing.ThingId == id);
            if (thing == null)
            {
                return NotFound();
            }
            return thing;
        }

        [HttpPut]
        [Route("add")]
        public ActionResult AddProduct(Thing thing)
        {
            redBookContext.Thing.Add(thing);

            try
            {
                redBookContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400); // invalid request
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult RemoveProduct(int id)
        {
            var product = redBookContext.Thing.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            redBookContext.Thing.Remove(product);

            try
            {
                redBookContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public ActionResult ModifyProduct(int id, Thing thing)
        {
            var oldThing = redBookContext.Thing.Find(id);

            if (oldThing == null)
            {
                return NotFound();
            }

            oldThing.Name = thing.Name;
            oldThing.Description = thing.Description;
            oldThing.CategoryId = thing.CategoryId;

            try
            {
                redBookContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        
        [HttpGet("name/{name}")]
        public ActionResult<Thing> SearchByName(string name)
        {
            var thing = redBookContext.Thing.FirstOrDefault(thing => thing.Name == name);
            if (thing == null)
            {
                return NotFound();
            }
            return thing;
        }

        [HttpGet("category/{category}")]
        public ActionResult<Thing> SearchByCategory(int category)
        {
            var thing = redBookContext.Thing.FirstOrDefault(thing => thing.CategoryId == category);
            if (thing == null)
            {
                return NotFound();
            }
            return thing;
        }

    }
}
