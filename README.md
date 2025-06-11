# Kurs Kayıt Projesi - .NET Core Entity Framework Projesi

Bu proje, Entity Framework Core ile geliştirilmiş basit bir kurs kayıt ve takip sistemidir. Amaç, .NET mimarisine uygun olarak ve ilişkisel tablo yapısı ile CRUD işlemlerini gerçekleştirmektir.

## 🔧 Kullanılan Teknolojiler
- ASP.NET Core MVC
- Entity Framework Core
- SQLite Server
- Git & GitHub

## 📁 MVC Yapısı
- **Entities**: Tablo Yapısı
- **View**: Html Dosyaları
- **Controller**: CRUD işlemleri
- **Context**: Injection işlemi ve DbSetlerin tanımlanması

## 🧪 Özellikler
- Öğrenci, Eğitmen ve Kurs ekleme, silme, güncelleme, listeleme
- Eğitmen ve Kurs bazlı filtreleme

## 🚀 Nasıl Çalıştırılır?
1. `appsettings.json` üzerinden veritabanı bağlantısı yapılandırılır.
2. `dotnet ef database update` ile veritabanı oluşturulur.
3. `dotnet run` komutu ile proje başlatılır.

