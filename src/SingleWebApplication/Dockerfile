#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM swacli/static-web-apps-cli:1.1.7 AS base
WORKDIR /home/site/wwwroot
EXPOSE 4280

COPY . .

CMD ["swa", "start"]