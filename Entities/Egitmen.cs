using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Entities
{
    public class Egitmen
    {
        public int Id { get; set; }
        public string? AdSoyad { get; set; }
        public string EPosta { get; set; }
        public string Telefon { get; set; }
        public ICollection<Kurs> Kurslar { get; set; }= new List<Kurs>();
    }
}