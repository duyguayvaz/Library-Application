
# 📚 Library Manager Application

Bu proje, basit bir **kütüphane yönetim sistemi** geliştirmeyi amaçlar. Kullanıcılar kitap ekleyebilir, silebilir, güncelleyebilir ve arayabilir. Uygulama sade ve kullanıcı dostu bir arayüze sahiptir.

---

## 🔐 Uygulama Giriş Şifresi

```
Şifre: 5355
```

---

## 🗂️ Proje Yapısı

```bash
Library-Application/
├── LibraryApp-Application/     # Uygulamanın kullanıcı arayüzü (WinForms)
├── LibraryApp-DLL/             # İş mantığını içeren DLL projesi
├── lib_manager_app_setup/      # Kurulum (setup) dosyaları
├── docs/                       # Geliştirici dökümantasyonları (Doxygen)
├── doxyfile_dev                # Doxygen yapılandırma dosyası
├── doxyfile_dev_run.bat        # Belgeleri oluşturmak için çalıştırılabilir bat dosyası
├── ce103-hw3.sln               # Visual Studio çözüm dosyası
└── .git/                       # Versiyon kontrol klasörü
```

---

## ⚙️ Kurulum ve Çalıştırma

### ✅ Gereksinimler

- Windows işletim sistemi
- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) veya üzeri
- .NET Framework 4.7.2 veya uyumlu sürüm

### 🧩 Kurulum (Setup)

1. `lib_manager_app_setup` klasörüne gidin.
2. `setup.exe` dosyasını çalıştırın.
3. Kurulum adımlarını takip ederek uygulamayı yükleyin.
4. Kurulum sonrası masaüstüne kısayol oluşacaktır.

> **Not:** Kurulum gerekmiyorsa aşağıdaki "Visual Studio ile Çalıştırma" adımlarını izleyebilirsiniz.

---

## 💻 Visual Studio ile Çalıştırma

1. `ce103-hw3.sln` dosyasını Visual Studio ile açın.
2. Sol taraftaki "Solution Explorer" bölümünde `LibraryApp-Application` projesine sağ tıklayın → **Set as StartUp Project** seçin.
3. Derleme için `Ctrl + Shift + B` kısayolunu kullanın.
4. Uygulamayı çalıştırmak için `F5` tuşuna basın.
5. Açılan ekrana şifreyi girin: `5355`

---

## 🔍 Özellikler

- 📘 Kitap ekleme, silme ve güncelleme
- 📚 Tüm kitapları listeleme
- 🔎 Kitap adına göre arama
- 💾 Verileri saklama
- 🖥️ Basit, anlaşılır kullanıcı arayüzü

---

## 📄 Geliştirici Belgeleri

Proje içinde `doxyfile_dev` ve `doxyfile_dev_run.bat` dosyaları ile Doxygen dokümantasyonu oluşturulabilir.

### Belge oluşturmak için:

```bash
doxyfile_dev_run.bat
```

dosyasını çalıştırın. Belgeler `docs/` klasöründe yer alacaktır.

---

## 📜 Lisans

Bu proje yalnızca eğitim amaçlı geliştirilmiştir. Herhangi bir ticari kullanım amacı taşımaz.
