using EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Context;

    public class DataContext : DbContext
    {

    public DataContext(DbContextOptions<DataContext>  options) : base(options)
    {
    }

    protected DataContext()
    {
    }

    public DbSet<Kurs> Kurslar { get; set; }
        public DbSet<KursKayit> KursKayitlari { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Egitmen> Egitmenler { get; set; }
    }
