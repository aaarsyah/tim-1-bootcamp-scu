Apple Music - Final Project Tim 1

**Kelompok 1**
1. Addinda Ayu Arsyah
2. Yussuf
3. Anbang

**Cara menjalankan untuk production**

Buat docker compose untuk proyek
```
docker-compose -f docker-compose.yml -p bootcamp-project up -d
```

Database yang dihasilkan dari docker akan dalam keadaan kosong, jadi database perlu di-seeding

Untuk menghasilkan script mentah untuk database seeding, jalankan command ini
```
dotnet ef migrations script --startup-project ../06.WebAPI
```

**Cara menjalankan untuk development**

Jalankan command dalam direktori `src\05.Infrastructure`
```
dotnet ef database update --startup-project ../06.WebAPI
```
(Bila tidak bisa, setup SQL Server terlebih dahulu, lalu set connection string sesuai dengan credential SQL Server di `"src\06.WebAPI\appsettings.Development.json"`)

Build proyek lalu run
```
dotnet build
dotnet run
```

**Untuk development**

Buat docker compose untuk sonarqube
```
docker-compose -f docker-compose-sonarqube.yml -p BootcampProjectSonarQube up -d
```

Setelah sonarqube berjalan, run `sonar-scan.bat` untuk meng-scan code dan run unit test untuk code coverage

