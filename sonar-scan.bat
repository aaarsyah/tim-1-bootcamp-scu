REM set token disini
set /A TOKEN=sqp_0123456789abcdef0123456789abcdef01234567

REM pre-run cleanup
dotnet clean MyApp.sln

dotnet sonarscanner begin ^
/k:"BootcampProject" ^
/d:sonar.host.url="http://localhost:9000" ^
/d:sonar.token="%TOKEN%" ^
/d:sonar.cs.opencover.reportsPaths="TestResults/**/coverage.opencover.xml" ^
/d:sonar.cs.vstest.reportsPaths="TestResults/**/*.trx" ^
/d:sonar.coverage.exclusions="**/Migrations/**,**/wwwroot/**,**/*.cshtml,**/Program.cs" ^
/d:sonar.cpd.exclusions="**/Migrations/**,**/wwwroot/**,**/*.cshtml" ^
/d:sonar.exclusions="**/wwwroot/**,**/obj/**,**/bin/**,**/docker-compose.yml,**/sonar-scan.bat"

dotnet build MyApp.sln --configuration Release

dotnet test MyApp.sln --configuration Release --no-build --logger "trx" --results-directory TestResults ^
--collect:"XPlat Code Coverage" ^
-- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

dotnet sonarscanner end /d:sonar.token="%TOKEN%"

REM post-run cleanup
rmdir TestResults /S /Q

pause
