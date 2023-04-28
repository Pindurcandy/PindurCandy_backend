using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PindurCandy.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Data;

namespace PindurCandy.Controllers
{
    internal class UserData
    {
        public string Uid;
        public string TeljesNev;
        public string Jogosultsag;
        public byte[] Image;

    }

    [Route("[controller]")]
    [ApiController]
   

    public class LoginController : ControllerBase
    {
        [HttpPost("SaltRequest/{nev}")]

        public IActionResult SaltRequest(string nev)
        {
            using (var context = new candyshopContext())
            {
                try
                {
                    List<Felhasznalok> talalat = new List<Felhasznalok>(context.Felhasznaloks.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0)
                    {
                        return Ok(talalat[0].Salt);
                    }
                    else
                    {
                        return BadRequest("Hibás felhasználónév!");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }
        [HttpPost]

        public JsonResult Login(string nev, string tmpHash)
        {
            using (var context = new candyshopContext())
            {
                UserData data = new UserData();
                try
                {
                    List<Felhasznalok> talalat = new List<Felhasznalok>(context.Felhasznaloks.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0 && talalat[0].Aktiv == 1)
                    {
                        //Egyszerre csak egy gépről lehet dolgozni eleje
                        bool talalt = false;
                        int index = 0;
                        int elemSzam = Program.LoggedInUsers.Count;
                        while (!talalt && index < elemSzam)
                        {
                            if (Program.LoggedInUsers.ElementAt(index).Value.FelhasznaloNev == nev)
                            {
                                lock (Program.LoggedInUsers)
                                {
                                    Program.LoggedInUsers.Remove(Program.LoggedInUsers.ElementAt(index).Key);
                                }
                                talalt = true;
                            }
                            index++;
                        }
                        //Egyszerre csak egy gépről lehet dolgozni vége
                        string hash = PindurCandy.Program.CreateSHA256(tmpHash);
                        
                        if (hash == talalat[0].Hash)
                        {
                            string token = Guid.NewGuid().ToString();
                            lock (Program.LoggedInUsers)
                            {
                                Program.LoggedInUsers.Add(token, talalat[0]);
                            }
                            data.Uid = token;
                            data.TeljesNev = talalat[0].TeljesNev;
                            data.Jogosultsag = talalat[0].Jogosultsag.ToString();
                            data.Image = talalat[0].Image;
                            //string[] response = new string[3] { token, talalat[0].TeljesNev, talalat[0].Jogosultsag.ToString() };
                            
                            //return StatusCode(200, data);
                            //return Ok(data);
                        }
                        else
                        {
                            data.Uid = "Hibás jelszó!";
                            data.TeljesNev = "";
                            data.Jogosultsag = "-1";
                            //return Ok(data);
                        }
                    }
                    else
                    {
                        data.Uid = "Hibás név/Inaktív felhasználó!";
                        data.TeljesNev = "";
                        data.Jogosultsag = "-1";
                        //return Ok(data);
                    }
                }
                catch (Exception ex)
                {
                    data.Uid = ex.Message;
                    data.TeljesNev = "";
                    data.Jogosultsag = "-1";
                    //return Ok(data);
                }
                return new JsonResult(JsonConvert.SerializeObject(data,Formatting.Indented));
            }
        }
    }
}
