# 📧 M&Y Akademi - Email Projesi
Bu proje, .NET Core ile geliştirilmiş tam işlevsel bir e-posta yönetim sistemidir. Kullanıcılar kayıt olabilir, giriş yapabilir, e-posta gönderebilir, alabilir, silebilir ve okundu olarak işaretleyebilir. Proje, Entity Framework Core ve Identity altyapısı ile desteklenmektedir.

# 🔑 Özellikler
✅ Kullanıcı Kaydı ve Girişi (Register/Login)

🔐 Şifrelerin Hash'lenmiş şekilde veritabanında saklanması

👤 Profil sayfasında kullanıcının adı, soyadı, e-posta adresi, kullanıcı adı ve profil fotoğrafı

📊 Kullanıcının bugüne kadar gönderdiği ve aldığı e-posta sayılarının görüntülenmesi

✏️ Profil bilgilerini güncelleme

📨 Yeni mesaj gönderme

📥 Gelen kutusu (Inbox)

📤 Gönderilenler klasörü (Sent)

🗑️ Silinenler klasörü (Trash)

📬 Okunan ve okunmayan mesaj yönetimi

🔎 Konuya göre arama (Contains filtreleme)

📌 Sidebar'da dinamik mesaj sayıları (Okunmamış, silinmiş vb.)

⚙️ EF Core ile veri yönetimi, AppDbContext ve Migrations

# 🛠️ Teknolojiler
ASP.NET Core MVC

Entity Framework Core

ASP.NET Identity

MS SQL Server

Bootstrap 4

LINQ

Razor Views

# 💾 Veritabanı Yapısı
E-posta verileri EmailMessages tablosunda saklanmaktadır.

IsRead: Mesaj okunduysa true

IsDeleted: Mesaj silindiyse true

SenderEmailAddress, RecipientEmailAddress, EmailSubject, EmailBody gibi alanlar mevcuttur.

Kullanıcı bilgileri ise AspNetUsers tablosunda tutulur. Şifreler Identity ile hash'lenerek saklanır.

# 📌 Ek Notlar
Giriş yapmış kullanıcının bilgileri User.Identity.Name üzerinden alınır.

E-posta gönderirken SenderEmailAddress, giriş yapan kullanıcıdan otomatik çekilir.

ViewBag üzerinden gelen ve giden mesaj sayıları dinamik olarak gösterilir.

# 🖼️ Ekran Görüntüleri
## 📸 Giriş Sayfası
![Screenshot 2025-05-09 103427](https://github.com/user-attachments/assets/ec7e1319-66fe-4b80-992a-db783e4b593b)
![login](https://github.com/user-attachments/assets/54e3fe60-f155-45f5-aee8-af4370de1bb2)

## 📸 Profil Sayfası
![profile and logout](https://github.com/user-attachments/assets/04797623-6f20-4df1-9e96-2c2a243fd6d0)

## 📸 Gelen Kutusu
![inbox](https://github.com/user-attachments/assets/b2b0c598-a759-4000-bca6-db5ab2ac26a0)
![inbox search](https://github.com/user-attachments/assets/8cc41c8b-6c4a-4a8e-81ca-06e9671f4ec8)
![inbox2](https://github.com/user-attachments/assets/652ea08b-2e8b-4894-a57d-b9fb12b16a9c)

## 📸 Gönderilen Kutusu
![sent](https://github.com/user-attachments/assets/6439bc4b-c1ea-456e-b461-2c2e40173a2b)
![Screenshot 2025-05-09 105112](https://github.com/user-attachments/assets/3def9a00-8817-4857-bbbc-2fcc052aa9c9)
![sent2](https://github.com/user-attachments/assets/e43fbe43-1599-4641-9ab5-9d2dca313755)

## 📸 Yeni Mesaj Oluşturma
![compose](https://github.com/user-attachments/assets/7574ff0d-47f2-42a4-afcb-f0333fe78f6b)
![compose sweetalert](https://github.com/user-attachments/assets/dcb4cffd-ebe5-46e0-b553-72ae3dffda10)

## 📸 Mesajın Detayları
![Screenshot 2025-05-09 105409](https://github.com/user-attachments/assets/7fe7c1c0-7f69-4f62-b6bc-d84d357ac2cd)

## 📸 Silinenler klasörü
![trashbox](https://github.com/user-attachments/assets/97ed7b5c-1130-492e-b26b-c544610e1386)
