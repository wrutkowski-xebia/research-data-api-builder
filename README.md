#### March 2024, DAB version 0.10.*
![DAB](/img/dab.png)

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

