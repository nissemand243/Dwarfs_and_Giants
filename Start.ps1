    mkdir /.local
    Write-Output '*' > .local/.gitignore
    
    $password = [guid]::NewGuid().ToString()
    
    Write-Output $password > .local/db_password.txt
    Write-Output "Server=localhost;Database=SE_training;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False" > .local/connection_string.txt

    Write-Host "Starting App"
    docker compose up
