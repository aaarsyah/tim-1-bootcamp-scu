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

Saat ini masih belum ada database seeding dalam docker, jadi database masih kosong

**Cara menjalankan untuk development**
Database seeding
```
dotnet ef database update
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

