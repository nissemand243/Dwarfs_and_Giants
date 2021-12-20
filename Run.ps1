if ($((docker ps -a | Select-String SE_training | Measure-Object -line).lines) -gt 0)
{
    $SQL = "false"
}
else
{
    $sql = "true"
}

$certPath = "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"

#If the file does not exist, create it.
if (-not(Test-Path -Path $certPath -PathType Leaf)) {
    try {
        dotnet dev-certs https --export-path $certPath --password localhost --trust
        Write-Host "The file [$certPath] has been created."
    }
    catch {
        throw $_.Exception.Message
    }
}
# If the file already exists, show the message and do nothing.
else {
    Write-Host "Cannot create a certificate because another certificate already exists."
}

$project = "Server"

if ($SQL -eq "true")
{
    $password = [guid]::NewGuid().ToString()

    Write-Host "Starting SQL Server"
    docker run --name "SE_training" -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

    Write-Host "Configuring Connection String"
    $connectionString = "Server=localhost;Database=SE_training;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"
    dotnet user-secrets init --project $project
    dotnet user-secrets set "ConnectionStrings:SE_training" "$connectionString" --project $project | Out-Null

    Write-Host "Seeding database"
    cd .\Infrastructure\
    dotnet ef --startup-project ..\Server\ database update | Out-Null
    cd ..
    
    $password = ""
    $connectionString = ""
}

Write-Host "Starting App"
dotnet run --project $project
