using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PindurCandy.Models;
using System;
using System.Linq;

namespace PindurCandy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        [HttpPost]

        public IActionResult Post(Registry registry)
        {
            using (var context = new candyshopContext())
            {
                try
                {
                    if (context.Felhasznaloks.Where(f => f.FelhasznaloNev == registry.FelhasznaloNev).ToList().Count != 0)
                    {
                        return StatusCode(210, "Létezik ilyen felhasználónév!");
                    }
                    if (context.Felhasznaloks.Where(f => f.Email == registry.Email).ToList().Count != 0)
                    {
                        return StatusCode(210, "Ezen az e-mail címen már regisztrálva van fiók!");
                    }
                    if (registry.FelhasznaloNev != "" && registry.Email != "")
                    {
                        registry.Key = Program.GenerateSalt();
                        context.Add(registry);
                        context.SaveChanges();
                        Program.SendEmail(registry.Email, "Regisztráció", "A regisztráció befejezéséhez kattints a következő linkre: " + "http://localhost:5001/Registry/" + registry.Key);
                        return StatusCode(200, "Sikeres regisztráció! Az e-mail címre küldött utasítások alapján befejezheti azt");
                    }
                    else
                    {
                        return StatusCode(200, "Ellenőrzés lefuttatva");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(200, ex.Message);
                }
            }
        }

        [HttpGet("{Key}")]

        public IActionResult Get(string Key)
        {
            using (var context = new candyshopContext())
            {
                try
                {
                    var RegistryUser = context.Registries.Where(c => c.Key == Key).ToList();
                    if (RegistryUser.Count != 0)
                    {
                        Felhasznalok felhasznalo = new Felhasznalok();
                        felhasznalo.FelhasznaloNev = RegistryUser[0].FelhasznaloNev;
                        felhasznalo.Salt = RegistryUser[0].Salt;
                        felhasznalo.TeljesNev = RegistryUser[0].TeljesNev;
                        //felhasznalo.Image = RegistryUser[0].Image
                        //felhasznalo.Image = new byte[1];
                        felhasznalo.Hash = RegistryUser[0].Hash;
                        felhasznalo.Email = RegistryUser[0].Email;
                        felhasznalo.Aktiv = 1;
                        context.Felhasznaloks.Add(felhasznalo);
                        context.Registries.Remove(RegistryUser[0]);
                        context.SaveChanges();
                        return Ok("A regisztráció befejezve!");
                    }
                    else
                    {
                        return BadRequest("A regisztráció már mehgtörtént vagy sikeres kulcs történt megadásra!");
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }

            }
        }
    }
}
