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
      return await _db.Animals.ToListAsync();
    }
    [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal animal)
    {
      _db.Animals.Add(animal);
      await _db.SaveChangesAsync();
      return CreatedAtAction("Post",new {id =animal.AnimalId},animal);
    }    
  }
}