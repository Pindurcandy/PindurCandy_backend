using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PindurCandy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PindurCandy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FelhasznalokController : ControllerBase
    {
        [Route("basic")]
        [HttpGet]

        public IActionResult GetBasic()
        {

            using (var context = new candyshopContext())
            {
                try
                {
                    var response = context.Felhasznaloks.ToList();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{uId}")]

        public IActionResult Get(string uId)
        {
            
            using (var context = new candyshopContext())
            {
                try
                {
                    List<Felhasznalok> felhasznaloks = new List<Felhasznalok>(context.Felhasznaloks);
                    return Ok(felhasznaloks);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
           
        }



        [HttpPost]

        public IActionResult Post(Felhasznalok felhasznalo)
        {

            using (var context = new candyshopContext())
            {
                try
                {
                    context.Felhasznaloks.Add(felhasznalo);
                    context.SaveChanges();
                    return Ok("Új felhasználó adatai rögzítésre kerültek.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]

        public IActionResult Put(Felhasznalok felhasznalo)
        {

            using (var context = new candyshopContext())
            {
                try
                {

                    context.Felhasznaloks.Update(felhasznalo);
                    context.SaveChanges();
                    return StatusCode(200, "A felhasználó adatainak a módosítása megtörtént.");
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
                    Felhasznalok felhasznalo = new Felhasznalok();
                    felhasznalo.Id = id;
                    context.Felhasznaloks.Remove(felhasznalo);
                    context.SaveChanges();
                    return Ok("A felhasználó adatai törlésre kerültek.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
    }
}
