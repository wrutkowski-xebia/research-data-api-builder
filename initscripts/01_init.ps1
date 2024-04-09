# Some configuration needed to be done before running locally.

docker swarm init

Write-Host "Init connection string"
Write-Host "echo ""connection string"" | docker secret create database_connection_string -"
Write-Host "Remove if needed"
Write-Host "docker secret rm database_connection_string"

