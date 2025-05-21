using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Entities
{
    public class Kurs
    {
        public int Id { get; set; }
        public string? Baslik { get; set; }
        public int EgitmenId { get; set; }
        public Egitmen Egitmen { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; }= new List<KursKayit>();
    }
}