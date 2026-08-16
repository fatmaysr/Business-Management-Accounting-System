#  Nexus — İşletme Yönetim Sistemi

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4.svg)](https://dotnet.microsoft.com/)
[![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC%205-0D6EFD.svg)](https://dotnet.microsoft.com/apps/aspnet/mvc)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.0-68217A.svg)](https://learn.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927.svg)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3.svg)](https://getbootstrap.com/)
[![Unity](https://img.shields.io/badge/DI-Unity-333333.svg)](https://unitycontainer.org/)

**Nexus**, küçük ve orta ölçekli işletmelerin günlük finansal ve operasyonel süreçlerini tek bir sistem üzerinden takip edebilmesi amacıyla geliştirilmiş, **ASP.NET MVC 5 tabanlı N-katmanlı bir işletme yönetim uygulamasıdır.**

Sistem; **kasa, banka, cari hesap, stok ve satış** modüllerini birbirleriyle ilişkili şekilde çalıştırmayı hedefler.

Örneğin bir perakende satış gerçekleştirildiğinde yalnızca satış kaydı oluşturulmaz; satış tutarı aynı zamanda ilgili kasaya otomatik olarak **giriş hareketi** olarak yansıtılır.

> 

---

##  Öne Çıkan Özellikler

###  Kimlik Doğrulama & Kullanıcı Yönetimi

* Forms Authentication altyapısı
* BCrypt ile güvenli şifre hashleme
* `AntiForgeryToken` ile CSRF koruması
* Kullanıcı ekleme, düzenleme ve silme
* Basitleştirilmiş şifre sıfırlama akışı
* Yönetici işlemleri için loglama altyapısı

###  Cari / Müşteri Yönetimi

* Cari hesap oluşturma ve yönetme
* Müşteri listeleme ve düzenleme
* Cari hesap hareketlerini görüntüleme
* Cari bazlı filtreleme
* Tarih aralığına göre hareket raporlama
* Cari verilerini Excel'e aktarma

###  Kasa Yönetimi

* Birden fazla nakit kasa tanımlama
* Kasa giriş / çıkış hareketleri
* Günlük kasa görünümü
* Para birimine göre bakiye takibi
* Kasa hareketlerinin raporlanması

**Modüller arası entegrasyon:**

```text
Perakende Satış
      │
      ▼
   Sale Kaydı
      │
      ▼
Kasa Giriş Hareketi
```

Yani satış modülü ile kasa modülü birbirinden bağımsız çalışmaz; gerçekleştirilen satış finansal harekete dönüştürülür.

###  Banka Yönetimi

* Banka tanımlama
* Banka hesabı oluşturma
* Banka hareketlerini kaydetme
* Hesap hareketlerini listeleme
* Finansal hareketlerin takip edilmesi

### 🛒 Perakende Satış — Hızlı Satış

Hızlı satış ekranı üzerinden:

```text
Kategori
   ↓
Ürün
   ↓
Ödeme Yöntemi
   ↓
Satış
   ↓
Sale Kaydı
   ↓
Kasa Hareketi
```

* Kategori bazlı ürün seçimi
* Hızlı ürün satışı
* Nakit / Kart ödeme seçimi
* Gerçek `Sale` kaydı oluşturma
* Satış sonrasında ilgili kasaya otomatik giriş oluşturma
* Perakende müşteri desteği
* Kategori ve ürünlerin veritabanından yönetilmesi

> Ödeme yöntemi seçimi şu an yalnızca işlem üzerinde tutulan bir **kayıt bilgisidir**. Herhangi bir banka veya ödeme kuruluşu API'sine bağlı değildir.

###  Stok & Toptan Satış

* Stok kalemi oluşturma
* Ürün bilgilerini güncelleme
* Miktar takibi
* Stok listeleme
* Toptan satış süreçleri için temel altyapı

> Perakende ürünleri (`FastSaleProduct`) ve stok ürünleri (`Stock`) mevcut yapıda ayrı kataloglar olarak tutulmaktadır.

###  Raporlama

#### Kasa Durum Raporu

* Para birimine göre toplam giriş
* Toplam çıkış
* Güncel bakiye
* Kasa bazlı finansal durum

#### Cari Hareket Raporu

* Cari bazlı hareket listesi
* Tarih aralığı filtreleme
* İşlem geçmişi
* Cari hesap hareketlerinin detaylandırılması

###  Dashboard

Ana panel üzerinden işletmenin genel durumu özetlenir:

* Aktif kasa sayısı
* Cari sayısı
* Kullanıcı sayısı
* Günlük hareket sayısı
* Temel finansal göstergeler

Ayrıca dashboard üzerinde **TCMB üzerinden alınan canlı döviz kuru bilgisi** gösterilir.

> Döviz kuru widget'ı yalnızca bilgilendirme amaçlıdır ve uygulamanın finansal hesaplama akışına dahil değildir.

---

#  Mimari

Nexus, sorumlulukların birbirinden ayrılması ve uygulamanın sürdürülebilir olması amacıyla **N-katmanlı (N-Layer) mimari** kullanılarak geliştirilmiştir.

```text
┌──────────────────────────────────────┐
│          GateGold / MVC              │
│        Presentation Layer            │
│ Controllers • Views • Models         │
└──────────────────┬───────────────────┘
                   │
                   ▼
┌──────────────────────────────────────┐
│          BusinessLayer               │
│ Business Rules • Services            │
│ Validation • Filters                │
└──────────────────┬───────────────────┘
                   │
                   ▼
┌──────────────────────────────────────┐
│        DataAccessLayer               │
│ Entity Framework 6                  │
│ Repository • DbContext • Migrations │
└──────────────────┬───────────────────┘
                   │
                   ▼
┌──────────────────────────────────────┐
│          EntityLayer                 │
│          Domain Entities             │
└──────────────────────────────────────┘
```

### Katmanlar

#### `GateGold`

Sunum katmanıdır.

* ASP.NET MVC 5
* Controllers
* Views
* ViewModels
* Authentication
* HTTP request / response yönetimi

#### `BusinessLayer`

Uygulamanın iş kurallarını ve servislerini içerir.

* Business Services
* İş kuralları
* FluentValidation
* Exception handling
* Interface / implementation ayrımı

#### `DataAccessLayer`

Veritabanı işlemlerinden sorumludur.

* Entity Framework 6
* Code First
* Generic Repository
* DbContext
* LINQ
* Database Migrations

#### `EntityLayer`

Uygulamanın domain modellerini içerir.

* Entity sınıfları
* Veritabanı modelleri
* Domain ilişkileri

#### `BusinessLayer.Tests`

Business Layer içerisinde bulunan kritik iş akışlarının test edilmesi amacıyla oluşturulmuştur.

---

#  Dependency Injection

Uygulamada **Unity Container** kullanılarak Dependency Injection uygulanmıştır.

Controller'lar ve servisler mümkün olduğunca somut implementasyonlara doğrudan bağımlı olmak yerine abstraction'lar üzerinden çalışır.

Örneğin:

```text
Controller
    ↓
IService
    ↓
Service
    ↓
IRepository
    ↓
Repository
```

Bu yaklaşım;

* Test edilebilirliği
* Bağımlılıkların yönetilebilirliğini
* Kodun sürdürülebilirliğini
* Katmanlar arasındaki bağımlılığın azaltılmasını

sağlamayı amaçlar.

---

#  Güvenlik & Altyapı

Projede temel web uygulaması güvenlik pratikleri uygulanmıştır.

### Authentication

ASP.NET Forms Authentication kullanılmaktadır.

### Password Hashing

Kullanıcı şifreleri düz metin olarak saklanmaz. **BCrypt** kullanılarak hashlenir.

### CSRF Protection

Form işlemlerinde:

```csharp
[ValidateAntiForgeryToken]
```

kullanılarak CSRF saldırılarına karşı koruma uygulanmıştır.

### Input Validation

Kullanıcıdan alınan veriler **FluentValidation** ile doğrulanmaktadır.

### Centralized Error Handling

Yakalanmayan uygulama hataları merkezi hata yönetimi üzerinden ele alınır.

Hata bilgileri:

```text
Application Error
      │
      ├── Log File
      │
      └── AdminLog (Database)
```

şeklinde kayıt altına alınabilir.

---

#  Kullanılan Teknolojiler

| Alan                 | Teknoloji                              |
| -------------------- | -------------------------------------- |
| Backend              | ASP.NET MVC 5                          |
| Runtime              | .NET Framework 4.8                     |
| ORM                  | Entity Framework 6                     |
| Database             | Microsoft SQL Server                   |
| Architecture         | N-Layer Architecture                   |
| Repository           | Generic Repository Pattern             |
| Dependency Injection | Unity                                  |
| Validation           | FluentValidation                       |
| Authentication       | Forms Authentication                   |
| Password Hashing     | BCrypt.Net                             |
| Frontend             | Bootstrap 5                            |
| JavaScript           | jQuery                                 |
| Excel Export         | ClosedXML                              |
| Testing              | xUnit / MSTest                         |
| Database Management  | Entity Framework Code First Migrations |

---

#  Proje Yapısı

```text
Nexus/
│
├── GateGold/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── Filters/
│   └── ConnectionStrings.config.example
│
├── BusinessLayer/
│   ├── Abstract/
│   ├── Concrete/
│   ├── Filters/
│   └── ValidationRules/
│
├── DataAccessLayer/
│   ├── Abstract/
│   ├── Concrete/
│   ├── EntityFramework/
│   ├── Repositories/
│   └── Migrations/
│
├── EntityLayer/
│   └── Concrete/
│
├── BusinessLayer.Tests/
│
└── Nexus.sln
```

---

#  Kurulum

## Gereksinimler

Projeyi çalıştırmak için:

* Visual Studio 2019 / 2022
* .NET Framework 4.8
* SQL Server
* SQL Server Express veya LocalDB

yeterlidir.

---

## 1. Repoyu Klonla

```bash
git clone <repo-url>
cd Nexus
```

---

## 2. Solution'ı Aç

Visual Studio ile:

```text
Nexus.sln
```

dosyasını açın.

NuGet paketlerinin restore edilmesini bekleyin.

Gerekirse:

```text
Solution → Restore NuGet Packages
```

seçeneğini kullanabilirsiniz.

---

## 3. Connection String'i Yapılandır

Örnek bağlantı dosyasını kopyalayın:

```text
GateGold/ConnectionStrings.config.example
```

ve:

```text
GateGold/ConnectionStrings.config
```

olarak oluşturun.

Ardından SQL Server bilgilerinizi girin:

```xml
<connectionStrings>
    <add name="AppDbContext"
         connectionString="DATA SOURCE=SUNUCU_ADI\SQLEXPRESS;initial catalog=GateTest;Persist Security Info=True;User ID=KULLANICI_ADI;Password=SIFRE;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

>  Gerçek SQL Server kullanıcı adı ve şifrenizi GitHub'a commit etmeyin. `ConnectionStrings.config` dosyası repository dışında tutulmalıdır.

---

## 4. Veritabanını Oluştur

Visual Studio'da:

```text
Tools
→ NuGet Package Manager
→ Package Manager Console
```

açın.

Default Project olarak:

```text
DataAccessLayer
```

seçin.

Ardından:

```powershell
Update-Database
```

komutunu çalıştırın.

Entity Framework migrations mevcut veritabanı yapısını oluşturacaktır.

---

## 5. Uygulamayı Çalıştır

Startup Project olarak:

```text
GateGold
```

projesini seçin.

Ardından:

```text
F5
```

ile uygulamayı başlatın.

Başlangıçta:

```text
/Login
```

sayfasının açılması beklenir.

---

# 🧪 Test

Business Layer için test projesi:

```text
BusinessLayer.Tests
```

altında bulunmaktadır.

Testleri Visual Studio:

```text
Test
→ Test Explorer
```

üzerinden çalıştırabilirsiniz.

Özellikle satış akışı gibi iş kurallarının test edilmesi hedeflenmiştir.

Örnek senaryolar:

```text
Geçerli satış
     ↓
Sale oluştur
     ↓
Kasa hareketi oluştur
```

ve geçersiz senaryolarda beklenen exception / validation davranışlarının kontrol edilmesi.

---

# 📸 Ekran Görüntüleri

<img width="995" height="468" alt="Ekran görüntüsü 2026-08-16 215953" src="https://github.com/user-attachments/assets/86060964-220b-4952-9b6b-09807551ddc6" />



<img width="1908" height="1021" alt="Ekran görüntüsü 2026-08-16 220029" src="https://github.com/user-attachments/assets/7c1e6092-6e67-48f7-91b4-2666e5b89325" />





<img width="627" height="446" alt="Ekran görüntüsü 2026-08-16 220100" src="https://github.com/user-attachments/assets/af42dee6-f048-4a6f-92c8-c7b570c54ff6" />

---

#  Bilinen Sınırlamalar

Nexus'un kapsamı bilinçli olarak günlük işletme yönetimi ile sınırlandırılmıştır.

### Yetkilendirme

Şu an rol bazlı yetkilendirme bulunmamaktadır.

Giriş yapmış kullanıcılar aynı temel erişim seviyesine sahiptir.

### Ödeme Sistemleri

Nakit / Kart seçimi yalnızca satışın ödeme yöntemini kayıt altına alır.

Herhangi bir:

* POS API
* Banka API
* Ödeme gateway'i

entegrasyonu bulunmamaktadır.

### Şifre Sıfırlama

Şifre sıfırlama akışı basitleştirilmiş yapıdadır.

E-posta doğrulaması veya güvenli token tabanlı production-grade password recovery sistemi henüz bulunmamaktadır.

### E-Fatura / Resmi Muhasebe

Sistem:

* E-fatura
* E-arşiv
* E-irsaliye
* Mizan
* Bilanço
* Çift taraflı muhasebe
* Vergi dairesi entegrasyonu

gibi resmi muhasebe fonksiyonlarını içermez.

### Ürün Katalogları

Perakende satış ürünleri ile toptan stok ürünleri mevcut yapıda ayrı kataloglar olarak tutulmaktadır.

---

#  Yol Haritası

* [ ] Rol bazlı yetkilendirme — Admin / Kasiyer
* [ ] Perakende ve toptan ürün kataloglarının birleştirilmesi
* [ ] Genişletilmiş unit test kapsamı
* [ ] Güvenli e-posta tabanlı şifre sıfırlama
* [ ] Gelişmiş satış raporları
* [ ] Daha kapsamlı dashboard KPI'ları
* [ ] Ürün bazlı stok hareketlerinin genişletilmesi
* [ ] Excel / PDF raporlarının genişletilmesi

---

#  Projenin Amacı

Nexus yalnızca CRUD işlemlerinden oluşan bir yönetim paneli olarak değil, farklı modüllerin birbirleriyle **gerçek iş akışları üzerinden iletişim kurduğu** bir işletme yönetim sistemi olarak tasarlanmıştır.

Özellikle:

```text
Ürün
  ↓
Satış
  ↓
Sale
  ↓
Kasa Hareketi
  ↓
Finansal Rapor
```

gibi modüller arası bağlantılar, projenin temel tasarım hedeflerinden biridir.

Bu yapı sayesinde proje içerisinde:

* N-Layer Architecture
* Dependency Injection
* Repository Pattern
* Entity Framework Code First
* Database Migrations
* Business Rules
* Validation
* Authentication
* Logging
* Unit Testing
* Module Integration

gibi gerçek bir backend uygulamasında kullanılan farklı yazılım geliştirme pratikleri bir arada uygulanmaktadır.

---

