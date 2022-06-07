using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CretaceousPark.Models;

namespace Cretaceous.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AnimalsController :Controller
  {
    private readonly CretaceousParkContext _db;
    public AnimalsController(CretaceousParkContext db)
    {
      _db =db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> Get()
    {
      return await  _db.Animals.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal animal)
    {
      _db.Animals.Add(animal);
      await _db.SaveChangesAsync();
      return CreatedAtAction("Post", new {id =animal.AnimalId, animal});
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
      var animal= await _db.Animals.FindAsync(id);
      if(animal == null)
      {
        return NotFound();
      }
      return animal;
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Put( int id, Animal animal)
    {
      if(id!= animal.AnimalId)
      {
        return BadRequest();

      }
      _db.Entry(animal).State =EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch(DbUpdateConcurrencyException)
      {
        if(!AnimalExists(id))
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
    private bool AnimalExists( int id)

    {
      return _db.Animals.Any(model=>model.AnimalId==id);
      
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAnimal(int id)
    {
      var animal = await _db.Animals.FindAsync(id);
      if(animal == null)
      {
        return NotFound();
      }

      _db.Animals.Remove(animal);
      await _db.SaveChangesAsync();

      return NoContent();

    }


    // private readonly CretaceousParkContext _db;
    // public AnimalsController(CretaceousParkContext db)
    // {
    //   _db =db;
    // }
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Animal>>> Get()
    // {
    //   return await _db.Animals.ToListAsync();
    // }
    // [HttpPost]
    // public async Task<ActionResult<Animal>> Post(Animal animal)
    // {
    //   _db.Animals.Add(animal);
    //   await _db.SaveChangesAsync();
    //   return CreatedAtAction("Post",new {id =animal.AnimalId},animal);
    // } 
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Animal>> GetAnimal( int id) 
    // {
    //   var animal = await _db.Animals.FindAsync(id);
    //   if(animal ==null)
    //   {
    //     return NotFound();
    //   }
    //   return animal;
    // }  

  }
}