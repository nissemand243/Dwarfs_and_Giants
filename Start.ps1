dotnet dev-certs https --clean
dotnet dev-certs https --export-path $env:USERPROFILE\.aspnet\https\aspnetapp.pfx --password localhost --trust
dotnet dev-certs https --trust

mkdir /.local
Write-Output '*' > .local/.gitignore

$password = [guid]::NewGuid().ToString()

Write-Output $password > .local/db_password.txt
Write-Output "Server=localhost;Database=SE_training;User Id=sa;Password=$password" > .local/connection_string.txt

Write-Host "Starting App"
#docker-compose -f "docker-compose.yml" up -d
docker compose up
Write-Host "Now listening on: https://localhost:7018"
