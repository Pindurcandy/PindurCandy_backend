using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PindurCandy.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PindurCandy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TermekekController : ControllerBase
    {
        [Route("basic")]
        [HttpGet]

        public async Task<IActionResult> GetBasic()
        {

            using (var context = new candyshopContext())
            {
                try
                {
                    var response = await context.Termekeks.ToListAsync();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpPost]

        public IActionResult Post(Termekek termek)
        {

            using (var context = new candyshopContext())
            {
                try
                {
                    context.Termekeks.Add(termek);
                    context.SaveChanges();
                    return Ok("Új termék adatai rögzítésre kerültek.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]

        public IActionResult Put(Termekek termek)
        {

            using (var context = new candyshopContext())
            {
                try
                {
                    context.Termekeks.Update(termek);
                    context.SaveChanges();
                    return StatusCode(200, "A termék adatainak a módosítása megtörtént.");
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }

        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            using (var context = new candyshopContext())
            {
                try
                {
                    Termekek termek = new Termekek();
                    termek.Id = id;
                    context.Termekeks.Remove(termek);
                    context.SaveChanges();
                    return Ok("A termék adatai törlésre kerültek.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
    }
}
