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
- SWA CLI local + DAB Docker Container
- SWA CLI Docker Container (integrated DAB)
- SWA CLI Docker Container (only app) + DAB Docker Container ?


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
 
### More about using containers for DAB
- **No one way do do it right.**
- [Azure Container Instances](https://learn.microsoft.com/en-us/azure/container-instances/container-instances-update#limitations) - lack of later update capabilities of already running container (ex. same IP address not guarantee). Seems that later maintenance could be problematic when deploying this way. Instances service looks like more to be used as one-off scenario.
- Azure Container Apps or App Service should be better choice for long term use.
- Azure Container Apps offer to much, for this scenario needs, and on the other hand Azure Container Instances don't have everything needed.
- Maybe even better would be App Service.
- [Many deployment options of image.](https://learn.microsoft.com/en-us/azure/container-apps/code-to-cloud-options)


### From Dockerfile to Azure Container Apps - some not obvious settings
- Deploy GitHub Action: `appSourcePath: ${{ github.workspace }}` as absolute path, `dockerfilePath`, `buildArguments` as relative paths
- Azure Container Apps: Ingress: Target port 5000
- Connection string settings/format issues, this was tested and works: `Data Source=server,1433;Initial Catalog=db;User ID=user;Password=password`

### Secrets with Visual Studio 2022 + Containers + Azure Cloud

|[type](https://docs.docker.com/compose/compose-file/09-secrets/)| VS | Docker | Azure Container Apps |
|-|-|-|-|
|environment|don't support swarm|in swarm mode|environment secrets|
|file|don't support swarm|in swarm mode|secrets mount as volume|
|CLI docker secret|don't support swarm|in swarm mode|n/a? not swarm mode?|

Data API Builder uses `@env("env_name")` in configuration files and ex. connection string seems to be stored as environment variable. Where in Azure there seems no problem to store it as a secret, there could be issue on local development, because Docker needs swarm mode and VS out of the box support only Docker Compose. For local dev seems storing connection string in local env seems not best but solution. 
Seems in Azure build-in solution is working like equivalent for `/run/secrets/<name>` in pure docker.

[^1]: Local setup: Windows 10, Visual Studio 2022, Docker Desktop 4.28.0, WSL, Ubuntu 20.04 (WSL) (there were some issues with 22.04)
