using System.ComponentModel.DataAnnotations;
using EfCore.Entities;

namespace EfCore.Models
{
    public class KursViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Baslik { get; set; }
        public int EgitmenId { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; }= new List<KursKayit>();
        

    }
}