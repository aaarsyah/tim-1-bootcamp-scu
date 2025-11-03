dotnet sonarscanner begin /k:"BootcampProject" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_054db57f02859c748301883bb1b8f291a148195c" /d:sonar.coverage.exclusions="**/Migrations/**,**/wwwroot/**,**/*.cshtml,**/Program.cs" /d:sonar.exclusions="**/wwwroot/**,**/obj/**,**/bin/**,**/docker-compose.yml,**/sonar-scan.bat"

dotnet build default-project-main\MyApp.sln --configuration Release

dotnet test default-project-main\MyApp.sln --configuration Release --no-build --logger "trx"

dotnet sonarscanner end /d:sonar.token="sqp_054db57f02859c748301883bb1b8f291a148195c"

pause