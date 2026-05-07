using WoodX.API.Models;

namespace WoodX.API.Data;

public static class Seeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new User
                {
                    Name = "Admin",
                    Email = "admin@woodx.com.tr",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "admin",
                },
                new User
                {
                    Name = "Test Kullanıcı",
                    Email = "test@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("test123"),
                    Role = "customer",
                }
            );
            await db.SaveChangesAsync();
        }

        if (!db.Products.Any())
        {
            db.Products.AddRange(
                // ── Kamelya & Pergola ──────────────────────────────────────
                new Product
                {
                    Name = "Premium Ahşap Kamelya 3x4m",
                    Category = "Kamelya & Pergola",
                    Price = 4850,
                    OldPrice = 5600,
                    Stock = 8,
                    Rating = 4.8,
                    ReviewCount = 124,
                    Featured = true,
                    Image = "https://images.unsplash.com/photo-1600585154526-990dced4db0d?w=400&h=400&fit=crop",
                    Description = "Yüksek kaliteli çam ahşabından üretilen, UV dayanımlı vernikli 3x4 metre kamelya. Monoblok çatı sistemi ile kurulumu kolay.",
                    Tags = new List<string> { "kamelya", "ahşap", "bahçe", "premium" },
                },
                new Product
                {
                    Name = "Lüks Pergola 4x5m Metal Ayaklı",
                    Category = "Kamelya & Pergola",
                    Price = 7200,
                    OldPrice = 8500,
                    Stock = 5,
                    Rating = 4.9,
                    ReviewCount = 87,
                    Featured = true,
                    Image = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400&h=400&fit=crop",
                    Description = "Alüminyum ayaklı, ahşap üst örtülü lüks pergola. Yağmur oluğu sistemi dahil. Her hava koşuluna uygun.",
                    Tags = new List<string> { "pergola", "lüks", "metal ayak", "geniş" },
                },
                new Product
                {
                    Name = "Bahçe Kamelya Seti 2x3m",
                    Category = "Kamelya & Pergola",
                    Price = 2950,
                    Stock = 15,
                    Rating = 4.5,
                    ReviewCount = 63,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=400&h=400&fit=crop",
                    Description = "Kompakt bahçeler için ideal, ekonomik kamelya seti. Oturma grubu dahil değildir.",
                    Tags = new List<string> { "kamelya", "kompakt", "ekonomik" },
                },
                new Product
                {
                    Name = "Modern Çerçeve Pergola",
                    Category = "Kamelya & Pergola",
                    Price = 5400,
                    OldPrice = 6100,
                    Stock = 6,
                    Rating = 4.7,
                    ReviewCount = 45,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1588880331179-bc9b93a8cb5e?w=400&h=400&fit=crop",
                    Description = "Minimalist tasarımlı modern çerçeve pergola. Çatı panelleri isteğe göre seçilebilir.",
                    Tags = new List<string> { "pergola", "modern", "minimalist" },
                },

                // ── Ahşap Deck & Teras ────────────────────────────────────
                new Product
                {
                    Name = "WPC Deck Panel 3m²",
                    Category = "Ahşap Deck & Teras",
                    Price = 680,
                    OldPrice = 820,
                    Stock = 200,
                    Rating = 4.6,
                    ReviewCount = 312,
                    Featured = true,
                    Image = "https://images.unsplash.com/photo-1558618047-3c8c76ca7d13?w=400&h=400&fit=crop",
                    Description = "Ahşap-plastik kompozit WPC deck panel. Su ve nem geçirmez, bakım gerektirmez. Fiyat 3m² için geçerlidir.",
                    Tags = new List<string> { "deck", "WPC", "kompozit", "su geçirmez" },
                },
                new Product
                {
                    Name = "Cumaru Doğal Ahşap Deck 3m²",
                    Category = "Ahşap Deck & Teras",
                    Price = 1150,
                    Stock = 80,
                    Rating = 4.9,
                    ReviewCount = 156,
                    Featured = true,
                    Image = "https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?w=400&h=400&fit=crop",
                    Description = "Doğal cumaru (Brezilya cevizi) ahşap deck. Son derece dayanıklı, tropik iklim ağacı. 25+ yıl ömür.",
                    Tags = new List<string> { "deck", "cumaru", "doğal ahşap", "dayanıklı" },
                },
                new Product
                {
                    Name = "Teras Döşeme Seti Eksiksiz",
                    Category = "Ahşap Deck & Teras",
                    Price = 3200,
                    OldPrice = 3800,
                    Stock = 20,
                    Rating = 4.7,
                    ReviewCount = 89,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1600047509807-ba8f99d2cdde?w=400&h=400&fit=crop",
                    Description = "10m² teras için eksiksiz döşeme seti. Altlık profiller, vidalar ve montaj kılavuzu dahil.",
                    Tags = new List<string> { "teras", "set", "döşeme", "komple" },
                },

                // ── Bahçe Çit & Parmaklık ─────────────────────────────────
                new Product
                {
                    Name = "Ahşap Bahçe Çiti 180cm (1m)",
                    Category = "Bahçe Çit & Parmaklık",
                    Price = 320,
                    Stock = 500,
                    Rating = 4.4,
                    ReviewCount = 218,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1416879595882-3373a0480b5b?w=400&h=400&fit=crop",
                    Description = "Çam ahşabından üretilmiş 180cm yüksekliğinde bahçe çiti. Fiyat 1 metre içindir.",
                    Tags = new List<string> { "çit", "bahçe", "çam", "gizlilik" },
                },
                new Product
                {
                    Name = "Dekoratif Parmaklık Sistemi",
                    Category = "Bahçe Çit & Parmaklık",
                    Price = 450,
                    OldPrice = 520,
                    Stock = 150,
                    Rating = 4.6,
                    ReviewCount = 94,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400&h=400&fit=crop",
                    Description = "Balkon ve teras için dekoratif ahşap parmaklık. Metal bağlantı elemanları dahil. Fiyat 1 metre içindir.",
                    Tags = new List<string> { "parmaklık", "balkon", "dekoratif" },
                },
                new Product
                {
                    Name = "Gizlilik Paneli 90x180cm",
                    Category = "Bahçe Çit & Parmaklık",
                    Price = 580,
                    Stock = 75,
                    Rating = 4.8,
                    ReviewCount = 47,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1416879595882-3373a0480b5b?w=400&h=400&fit=crop",
                    Description = "Preslenmiş ahşap çıtalı gizlilik paneli. Rüzgar ve göz önünden korur. Kolay monte edilir.",
                    Tags = new List<string> { "panel", "gizlilik", "çit", "kolay montaj" },
                },

                // ── Marangozluk & Mobilya ─────────────────────────────────
                new Product
                {
                    Name = "Bahçe Masa Takımı 6 Kişilik",
                    Category = "Marangozluk & Mobilya",
                    Price = 6800,
                    OldPrice = 7500,
                    Stock = 10,
                    Rating = 4.9,
                    ReviewCount = 201,
                    Featured = true,
                    Image = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=400&h=400&fit=crop",
                    Description = "6 kişilik masif tik ahşap bahçe masa takımı. 6 sandalye dahil. Yağ bazlı koruyucu ile işlenmiş.",
                    Tags = new List<string> { "masa", "sandalye", "tik", "6 kişilik" },
                },
                new Product
                {
                    Name = "Ahşap Bahçe Salıncağı",
                    Category = "Marangozluk & Mobilya",
                    Price = 2400,
                    Stock = 12,
                    Rating = 4.7,
                    ReviewCount = 68,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1558618047-3c8c76ca7d13?w=400&h=400&fit=crop",
                    Description = "2 kişilik ahşap bahçe salıncağı. Su geçirmez kumaş minder dahil. Çelik zincir bağlantılar.",
                    Tags = new List<string> { "salıncak", "bahçe", "2 kişilik", "minder" },
                },
                new Product
                {
                    Name = "Köşe Kanepe Seti Bahçe",
                    Category = "Marangozluk & Mobilya",
                    Price = 9200,
                    OldPrice = 10500,
                    Stock = 4,
                    Rating = 4.8,
                    ReviewCount = 33,
                    Featured = true,
                    Image = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=400&h=400&fit=crop",
                    Description = "L şeklinde bahçe köşe kanepe seti. Özel dış mekan minderleri dahil. Yağmur örtüsü ile teslim edilir.",
                    Tags = new List<string> { "kanepe", "köşe", "set", "dış mekan" },
                },

                // ── Aksesuar ──────────────────────────────────────────────
                new Product
                {
                    Name = "Ahşap Saksı Standı 3'lü Set",
                    Category = "Aksesuar",
                    Price = 280,
                    OldPrice = 340,
                    Stock = 120,
                    Rating = 4.5,
                    ReviewCount = 445,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1416879595882-3373a0480b5b?w=400&h=400&fit=crop",
                    Description = "Farklı yüksekliklerde 3 adet ahşap saksı standı. Hem iç hem dış mekanda kullanılabilir.",
                    Tags = new List<string> { "saksı", "stand", "set", "dekorasyon" },
                },
                new Product
                {
                    Name = "Güneş Enerjili Bahçe Aydınlatması (6'lı)",
                    Category = "Aksesuar",
                    Price = 420,
                    Stock = 200,
                    Rating = 4.3,
                    ReviewCount = 287,
                    Featured = false,
                    Image = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400&h=400&fit=crop",
                    Description = "Solar enerjili LED bahçe yolu aydınlatması. 6 adet fener. Su geçirmez IP65 sertifikalı.",
                    Tags = new List<string> { "aydınlatma", "solar", "LED", "bahçe" },
                }
            );

            await db.SaveChangesAsync();
        }
    }
}
