using System;
using System.Collections.Generic;

#nullable disable

namespace PindurCandy.Models
{
    public partial class Termekek
    {
        public int Id { get; set; }
        public string TermekNev { get; set; }
        public byte[] Kep { get; set; }
        public int Ar { get; set; }
        public string Leiras { get; set; }
        public int Aktiv { get; set; }
        public string Link { get; set; }
    }
}
