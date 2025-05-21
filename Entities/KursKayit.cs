namespace EfCore.Entities;

    public class KursKayit
    {
        public int Id { get; set; }
        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
