using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EfCore.Models;
using EfCore.Entities;
using EfCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EfCore.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger , DataContext context)
    {
        _context=context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetOgrenciler()
    {
        return View(await _context.Ogrenciler.ToListAsync());
    }

    public async Task<IActionResult> GetKurslar()
    {
        var kurslar= await _context.Kurslar.Include(a=>a.Egitmen).ToListAsync();
        return View(kurslar);
    }

    public async Task<IActionResult> GetKursKayitlari()
    {
        var kursKayitlar= await _context.KursKayitlari
        .Include(x=>x.Ogrenci)
        .Include(x=>x.Kurs)
        .ToListAsync();
        return View(kursKayitlar);
    }

    public async Task<IActionResult> GetEgitmenler()
    {
        var result=await _context.Egitmenler
        .Include(a=>a.Kurslar)
        .ToListAsync();
        return View(result);
    }
    public  IActionResult CreateOgrenci()=>View();

    [HttpPost]
    public  async Task<IActionResult> CreateOgrenci(Ogrenci model)
    {
    await _context.Ogrenciler.AddAsync(model);
    await _context.SaveChangesAsync();
    return RedirectToAction("GetOgrenciler");
    }


    public  async Task<IActionResult> CreateKurs()
    {
        ViewBag.Egitmenler=new SelectList(await _context.Egitmenler.ToListAsync(),"Id","AdSoyad");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateKurs(KursViewModel model)
    {
        if(ModelState.IsValid)
        {
        await _context.Kurslar.AddAsync(new Kurs{Id=model.Id, Baslik=model.Baslik, EgitmenId=model.EgitmenId});
        await _context.SaveChangesAsync();
        return RedirectToAction("GetKurslar");
        }
        ViewBag.Egitmenler=new SelectList(await _context.Egitmenler.ToListAsync(),"Id","AdSoyad");
        return View(model);
    }

    public async Task<IActionResult> CreateKursKayit()
    {
        ViewBag.Ogrenciler= new SelectList(await _context.Ogrenciler.ToListAsync(),"Id","AdSoyad");
        ViewBag.Kurslar= new SelectList(await _context.Kurslar.ToListAsync(),"Id","Baslik");
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateKursKayit(KursKayit model)
    {
        model.KayitTarihi=DateTime.Now;
        await _context.KursKayitlari.AddAsync(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetKursKayitlari");
    }

    public async Task<IActionResult> CreateEgitmen()
    {
        ViewBag.Kurslar=new SelectList(await _context.Kurslar.ToListAsync(),"Id","Baslik");
     return View();   
    }

    [HttpPost]
    public async Task<IActionResult> CreateEgitmen(Egitmen model)
    {
        await _context.Egitmenler.AddAsync(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetEgitmenler");
    }
    public async Task<IActionResult> EditOgrenci(int? id){
        if(id==null)
        {
            return NotFound();
        }
        var ogrenci= await _context
        .Ogrenciler
        .Include(a=>a.KursKayitlari) 
        .ThenInclude(a=>a.Kurs)
        .FirstOrDefaultAsync(a=>a.Id==id);
        if(ogrenci == null)
        {
            return NotFound();
        }
        return View(ogrenci);
    }
    [HttpPost]
    public async Task<IActionResult> EditOgrenci(int id, Ogrenci model)
    {
        if(id != model.Id)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            try
            {
                _context.Ogrenciler.Update(model);
                await _context.SaveChangesAsync();
            }
            catch(Exception)
           {
            if(!_context.Ogrenciler.Any(o=>o.Id==model.Id))
            {
                return NotFound();
            } 
           } 
           return RedirectToAction("GetOgrenciler");
        }
        return View(model);
        
    }


    public async Task<IActionResult> EditKurs(int? id)
    {
        ViewBag.Egitmenler=new SelectList(await _context.Egitmenler.ToListAsync(),"Id","AdSoyad");
        if(id==null)
        {
            return NotFound();
        }
        var result= await _context.Kurslar
        .Include(b=>b.KursKayitlari)
        .ThenInclude(a=>a.Ogrenci)
        .FirstOrDefaultAsync(a=>a.Id==id);
        if(result ==null)
        {
            return NotFound();
        }
        var kurs= new KursViewModel{
            Id=result.Id,
            Baslik=result.Baslik,
            EgitmenId=result.EgitmenId,
            KursKayitlari=result.KursKayitlari.ToList()
        };
        return View(kurs);
    }

    [HttpPost]
    public async Task<IActionResult> EditKurs(int id, KursViewModel model)
    {
        if(id != model.Id)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            try
            {
                _context.Kurslar.Update(new Kurs {Id= model.Id, Baslik=model.Baslik, EgitmenId=model.EgitmenId});
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
               if(!_context.Kurslar.Any(k=>k.Id==model.Id))
               {
                return NotFound();
               }
            }
        return RedirectToAction("GetKurslar");
        }
        return View(model);
    }


    public async Task<IActionResult> EditEgitmen(int? id)
    {
        if(id ==null)
        {
            return NotFound();
        }
        var result = await _context.Egitmenler.FindAsync(id);
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> EditEgitmen(Egitmen model)
    {
        if(model==null)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            try{
                 _context.Egitmenler.Update(model);
                 await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
        return RedirectToAction("GetEgitmenler");
    }
    public async Task<IActionResult> DeleteOgrenci(int? id)
    {
        if(id==null)
        {
            return NotFound();
        }
        var result= await _context.Ogrenciler.FindAsync(id);
        if(result ==null)
        {
            return NotFound();
        }
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteOgrenci([FromForm]int id)
    {
         var result = await _context.Ogrenciler.FindAsync(id);
         if(result == null)
         {
            return NotFound();
         }
          _context.Remove(result);
          await _context.SaveChangesAsync();
          return RedirectToAction("GetOgrenciler");
    }


    public async Task<IActionResult> DeleteKurs(int? id)
    {
        if(id==null)
        {
            return NotFound();
        }
        var result= await _context.Kurslar.FindAsync(id);
        if(result ==null)
        {
            return NotFound();
        }
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteKurs([FromForm]int id)
    {
        var result = await _context.Kurslar.FindAsync(id);
                 if(result == null)
         {
            return NotFound();
         }
        _context.Kurslar.Remove(result);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetKurslar");


    }

    public async Task<IActionResult> DeleteEgitmen(int? id)
    {
                if(id==null)
        {
            return NotFound();
        }
        var result = await _context.Egitmenler.FindAsync(id);
        if(result==null)
        {
            return NotFound();
        }
        return View(result);

    }

    [HttpPost]
    public async Task<IActionResult> DeleteEgitmen([FromForm]int id)
    {
        var result = await _context.Egitmenler.FindAsync(id);
        if(result==null)
        {
            return NotFound();
        }
        _context.Egitmenler.Remove(result);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetEgitmenler");
    }

    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
