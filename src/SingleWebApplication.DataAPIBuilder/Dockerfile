#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-databases/data-api-builder:0.10.21 AS base

ARG DAB_CONFIG

COPY ${DAB_CONFIG} /App/swa-db-connections/dab.config.json

######################################################################
#base image use ENTRYPOINT that can be used by CMD and /App is WORKDIR
#https://github.com/Azure/data-api-builder/blob/main/Dockerfile
#ENTRYPOINT ["dotnet", "Azure.DataApiBuilder.Service.dll"]
######################################################################

CMD ["--ConfigFileName", "./swa-db-connections/dab.config.json"]
