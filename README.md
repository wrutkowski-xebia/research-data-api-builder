![DAB](/img/dab.png)

# Updates
- March 2024 [^1]: checking DAB version 0.10.*

---

### Research of Data API builder
Check possibilities that Data API builder has to offer.

### It it possible to run API builder solution within Static Web App?
Yes / No ?  

- For deployed together with and on Static Web App, I would say NO for production use because of: cold start, Application Insights problems and at the moment of writing there are 322 open issues on MS DAB GitHub project.

### How about configuration Static Web App + DAB Docker Container?
- Big picture on diagram: SWA and DAB as container
- Starting from repository there are two GithHub actions:
  - One: deploy Blazor app to SWA
  - Second: build image from Dockerfile, put it in registry and publish on Azure Container Apps

### Azure Resources needed for this kind of solution
- Application Insights
- Static Web App
- SQL server
- SQL database  
- Optional (DAB in container):
  - Container App 
  - Container Apps Environment

### Azure Static Web Apps + Data API builder
- [Overview](https://learn.microsoft.com/en-us/azure/static-web-apps/database-overview)
- [SWA + DAB](https://learn.microsoft.com/en-us/azure/static-web-apps/database-azure-sql?tabs=bash&pivots=static-web-apps-rest)

### Links
- [Main Page](https://learn.microsoft.com/en-us/azure/data-api-builder/)
- [Install DAB CLI](https://learn.microsoft.com/en-us/azure/data-api-builder/get-started/get-started-with-data-api-builder)
- [Azure SQL Quick Start](https://learn.microsoft.com/en-us/azure/data-api-builder/get-started/get-started-azure-sql)
- [DAB CLI](https://learn.microsoft.com/en-us/azure/data-api-builder/data-api-builder-cli)
- [SWA CLI Docker](https://azure.github.io/static-web-apps-cli/docs/cli/docker)
- [DAB Docker Hub](https://hub.docker.com/_/microsoft-azure-databases-data-api-builder) - [Verified?](https://github.com/Azure/data-api-builder/discussions/2158)

### Some maybe not obvious/common topics
- Data API builder is hosted at static part of url: `www.site.com` +  **/data-api** + `/rest`
- custom path to DAB config need to be specified in Git Workflow at: data_api_location
- [OpenAPI definitions at `<url>/rest` + **/openapi**](https://learn.microsoft.com/en-us/azure/data-api-builder/openapi)  

### Issues
- [DAB CLI static port 5000 on localhost](https://github.com/Azure/data-api-builder/issues/1477)
Option to run it by Docker with custom port: 
`docker run -it --rm -v "%cd%\swa-db-connections:/App/swa-db-connections" -p 5033:5000 --env DATABASE_CONNECTION_STRING="%DATABASE_CONNECTION_STRING%" mcr.microsoft.com/azure-databases/data-api-builder:0.10.21 --ConfigFileName ./swa-db-connections/staticwebapp.database.config.json`
- [Application Insights currently working only on localhost](https://github.com/Azure/data-api-builder/issues/1735)
Deploying also with API, allows to turn on Insights setting on Azure, but that don't make Insights to log calls from DAB. Even if that setting is OFF, calls from localhost are logged.
- ["Cold Start" problems, giving 400 error on few first requests](https://github.com/Azure/data-api-builder/issues/918)
- Docker Hub image Verified?
- [Random timeouts?](https://github.com/Azure/data-api-builder/issues/2162)
 
### More about using containers for DAB
- **No one way do do it right.**
- [Azure Container Instances](https://learn.microsoft.com/en-us/azure/container-instances/container-instances-update#limitations) - lack of later update capabilities of already running container (ex. same IP address not guarantee). Seems that later maintenance could be problematic when deploying this way. Instances service looks like more to be used as one-off scenario.
- Azure Container Apps or App Service should be better choice for long term use.
- Azure Container Apps offer to much, for this scenario needs, and on the other hand Azure Container Instances don't have everything needed.
- Maybe even better would be App Service.
- [Many deployment options of image.](https://learn.microsoft.com/en-us/azure/container-apps/code-to-cloud-options)
- Application Insights seems working when running from container.


### From Dockerfile to Azure Container Apps - some not obvious settings
- Deploy GitHub Action: `appSourcePath: ${{ github.workspace }}` as absolute path, `dockerfilePath`, `buildArguments` as relative paths
- Azure Container Apps: Ingress: Target port 5000
- Connection string settings/format issues, this was tested and works: `Data Source=server,1433;Initial Catalog=db;User ID=user;Password=password` also can try to generate one from "Service Connector" (auto crate new entry in secrets)

### Secrets with Visual Studio 2022 + Containers + Azure Cloud

|[type](https://docs.docker.com/compose/compose-file/09-secrets/)| VS | Docker | Azure Container Apps |
|-|-|-|-|
|environment|don't support swarm|in swarm mode|environment secrets|
|file|don't support swarm|in swarm mode|secrets mount as volume|
|CLI docker secret|don't support swarm|in swarm mode|n/a? not swarm mode?|

Data API Builder uses `@env("env_name")` in configuration files and ex. connection string seems to be stored as environment variable. Where in Azure there seems no problem to store it as a secret, there could be issue on local development, because Docker needs swarm mode and VS out of the box support only Docker Compose. For local dev seems storing connection string in local env seems not best but solution. 
Seems in Azure build-in solution is working like equivalent for `/run/secrets/<name>` in pure docker.

### Authentication, Authorization (JWT)
- "Default" Azure App registration + Enterprise Application configuration for SWA + API
- Adding to request: JWT token, header X-MS-API-ROLE=role (seems token alone is not working)

---
### Local DEV: General Problems
- Port 5000 for DAB can't be changed.
- Blazor WASM can't be debug with containers? 
  - [#37565](https://github.com/dotnet/aspnetcore/issues/37565)
  - "Unsupported scenarios: Debug in non-local scenarios" [MS Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/debug?view=aspnetcore-8.0&tabs=visual-studio)



---

### Local DEV Concept: Blazor WASM + SWA CLI Docker
- VS don't support Swarm mode out of the box. This mode, would be nice here, mostly for using Secrets feature, not because of complexity of application etc.
- [Visual Studio 2022 Docker - MS Docs](https://learn.microsoft.com/en-us/visualstudio/containers/?view=vs-2022)
- Adding Docker to Blazor isn't supported thru project create wizard. It's need to be added later similar like for [React one](https://learn.microsoft.com/en-us/visualstudio/containers/container-tools-react?view=vs-2022).
- `Dockerfile` and `docker-compose` needs to customized as in this repo.
- Environment variable needs to be added: `DATABASE_CONNECTION_STRING`
- If DB is hosted on Azure check firewall settings.

### Local Dev Concept Problems: Blazor WASM + SWA CLI Docker
Seems making VS Docker Compose "hit F5 and debug/run" it's not so easy to make local dev easier, even its brings more challenges. There are some VS Docker Tools and SWA CLI issues around debugging and even executing app in container.

- SWA as container: [Access to the path '/home/vscode/.local/share/NuGet/Migrations' is denied.](https://github.com/Azure/static-web-apps-cli/discussions/824)
- [Access workaround?](https://github.com/microsoft/DockerTools/issues/399)
- Blazor Specific
  - [#37565](https://github.com/dotnet/aspnetcore/issues/37565)
  - [#49795](https://github.com/dotnet/aspnetcore/issues/49795)
  - [#27766](https://github.com/dotnet/aspnetcore/issues/27766)
- ~100 open VS Docker Tools [issues](https://github.com/microsoft/dockertools/issues), also last commit was about 10 months ago.
- ~100 open SWA CLI [issues](https://github.com/Azure/static-web-apps-cli/issues), last commit one month ago.
- .Net 8.0 for Blazor Wasm in SWA CLI Docker image [not supported now](https://github.com/Azure/static-web-apps-cli/discussions/825)

---

### Local Dev Concept: Blazor WASM + custom Dockerfile
Prepare specific Dockerfile with using ex. Microsoft ASP.Net Core docker images as a base for building, publishing and etc.

### Local Dev Concept Problems: Blazor WASM + custom Dockerfile
- [Debug](https://learn.microsoft.com/en-us/aspnet/core/blazor/debug?view=aspnetcore-8.0&tabs=visual-studio)

 
[^1]: Local setup: Windows 10, Visual Studio 2022 17.9.6, Docker Desktop 4.28.0, WSL, Ubuntu 20.04 (WSL) (there were some issues with 22.04), .Net 8.0
